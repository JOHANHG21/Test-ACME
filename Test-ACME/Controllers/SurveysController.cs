using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityApp.Data;
using Test_ACME.Models;
using Microsoft.AspNetCore.Authorization;

namespace Test_ACME.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SurveysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Surveys
        public async Task<IActionResult> Index()
        {
            return View(await _context.Surveys.ToListAsync());
        }

        // GET: Surveys/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Surveys/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Survey survey, List<SurveyField> fields)
        {
            if (string.IsNullOrEmpty(survey.UniqueLink))
            {
                survey.UniqueLink = Guid.NewGuid().ToString();
            }

            if (ModelState.IsValid)
            {
                // Generar el UniqueLink
                survey.UniqueLink = Guid.NewGuid().ToString();

                // Asociar los campos a la encuesta
                survey.Fields = fields;

                _context.Add(survey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(survey);
        }

        // GET: Surveys/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Incluir los campos y las respuestas asociadas
            var survey = await _context.Surveys
                .Include(s => s.Fields) // Incluye los campos actuales
                .ThenInclude(f => f.SurveyResponses) // Incluye respuestas asociadas a los campos
                .FirstOrDefaultAsync(s => s.Id == id);

            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        // POST: Surveys/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Survey survey, List<SurveyField> fields)
        {
            if (id != survey.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Recuperar la encuesta existente desde la base de datos
                    var existingSurvey = await _context.Surveys
                        .Include(s => s.Fields) // Incluye los campos actuales
                        .FirstOrDefaultAsync(s => s.Id == id);

                    if (existingSurvey == null)
                    {
                        return NotFound();
                    }

                    // Actualizar las propiedades principales de la encuesta
                    existingSurvey.Name = survey.Name;
                    existingSurvey.Description = survey.Description;

                    // Procesar campos existentes y nuevos
                    if (fields != null && fields.Any())
                    {
                        foreach (var field in fields)
                        {
                            if (field.Id == 0)
                            {
                                // Agregar nuevos campos
                                field.SurveyId = id;
                                _context.SurveyFields.Add(field);
                            }
                            else
                            {
                                // Actualizar campos existentes
                                var existingField = existingSurvey.Fields.FirstOrDefault(f => f.Id == field.Id);
                                if (existingField != null)
                                {
                                    existingField.Name = field.Name;
                                    existingField.Title = field.Title;
                                    existingField.FieldType = field.FieldType;
                                    existingField.IsRequired = field.IsRequired;
                                }
                            }
                        }

                        // Eliminar campos que no est�n en la lista actual
                        var fieldsToRemove = existingSurvey.Fields
                            .Where(existingField => !fields.Any(f => f.Id == existingField.Id))
                            .ToList();

                        _context.SurveyFields.RemoveRange(fieldsToRemove);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Surveys.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(survey);
        }

        // GET: Surveys/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Surveys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        // POST: Surveys/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var survey = await _context.Surveys.FindAsync(id);
            if (survey != null)
            {
                _context.Surveys.Remove(survey);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurveyExists(int id)
        {
            return _context.Surveys.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Results(int? id)
        {
            if (id == null)
            {
                return NotFound("La encuesta no existe.");
            }

            // Obtener la encuesta con sus campos y respuestas
            var survey = await _context.Surveys
                .Include(s => s.Fields)
                .ThenInclude(f => f.SurveyResponses) // Incluir las respuestas asociadas a los campos
                .FirstOrDefaultAsync(s => s.Id == id);

            if (survey == null)
            {
                return NotFound("La encuesta no existe.");
            }

            return View(survey);
        }

    }
}
