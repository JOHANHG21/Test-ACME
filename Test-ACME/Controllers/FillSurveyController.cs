using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityApp.Data;
using Test_ACME.Models;
using System.Threading.Tasks;

namespace Test_ACME.Controllers
{
    public class FillSurveyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FillSurveyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FillSurvey/{uniqueLink}
        [HttpGet]
        public async Task<IActionResult> Index(string uniqueLink)
        {
            if (string.IsNullOrEmpty(uniqueLink))
            {
                return BadRequest("El enlace proporcionado es inválido.");
            }

            var survey = await _context.Surveys
                .Include(s => s.Fields)
                .FirstOrDefaultAsync(s => s.UniqueLink == uniqueLink);

            if (survey == null)
            {
                return NotFound("La encuesta no existe o el enlace es inválido.");
            }

            return View(survey);
        }

        // POST: FillSurvey/{uniqueLink}
        [HttpPost]
        public async Task<IActionResult> Submit(string uniqueLink, Dictionary<int, string> responses)
        {
            if (string.IsNullOrEmpty(uniqueLink))
            {
                return BadRequest("El enlace proporcionado es inválido.");
            }

            // Obtener la encuesta con los campos
            var survey = await _context.Surveys
                .Include(s => s.Fields)
                .FirstOrDefaultAsync(s => s.UniqueLink == uniqueLink);

            if (survey == null)
            {
                return NotFound("La encuesta no existe o el enlace es inválido.");
            }

            // Guardar las respuestas enviadas
            foreach (var field in survey.Fields)
            {
                if (responses.ContainsKey(field.Id))
                {
                    var response = new SurveyResponse
                    {
                        SurveyId = survey.Id,
                        FieldId = field.Id,
                        Value = responses[field.Id]
                    };
                    _context.SurveyResponses.Add(response);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("ThankYou", new { uniqueLink });
        }


        public async Task<IActionResult> ThankYou(string uniqueLink)
        {
            if (string.IsNullOrEmpty(uniqueLink))
            {
                return BadRequest("El enlace proporcionado es inválido.");
            }

            Console.WriteLine($"El uniqueLink recibido en ThankYou es: {uniqueLink}");

            // Busca la encuesta en la base de datos
            var survey = await _context.Surveys.FirstOrDefaultAsync(s => s.UniqueLink == uniqueLink);
            if (survey == null)
            {
                return NotFound("La encuesta no existe o el enlace es inválido.");
            }

            ViewBag.SurveyName = survey.Name;
            return View();
        }
    }
}
