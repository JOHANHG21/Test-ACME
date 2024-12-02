using IdentityApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Test_ACME.Models;

namespace Test_ACME.Controllers
{
    public class FillSurveyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FillSurveyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar la encuesta basada en el UniqueLink
        [HttpGet("{uniqueLink}")]
        public async Task<IActionResult> Index(string uniqueLink)
        {
            // Buscar la encuesta por el UniqueLink
            var survey = await _context.Surveys
                .Include(s => s.Fields)
                .FirstOrDefaultAsync(s => s.UniqueLink == uniqueLink);

            if (survey == null)
            {
                return NotFound("La encuesta no existe o el enlace es inválido.");
            }

            return View(survey);
        }

        // Acción para guardar las respuestas
        [HttpPost("{uniqueLink}")]
        public async Task<IActionResult> Submit(string uniqueLink, Dictionary<int, string> responses)
        {
            // Buscar la encuesta por el UniqueLink
            var survey = await _context.Surveys
                .Include(s => s.Fields)
                .FirstOrDefaultAsync(s => s.UniqueLink == uniqueLink);

            if (survey == null)
            {
                return NotFound("La encuesta no existe o el enlace es inválido.");
            }

            // Guardar las respuestas
            foreach (var field in survey.Fields)
            {
                var response = new SurveyResponse
                {
                    SurveyId = survey.Id,
                    FieldId = field.Id,
                    Value = responses.ContainsKey(field.Id) ? responses[field.Id] : null
                };
                _context.Add(response);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("ThankYou");
        }

        // Pantalla de agradecimiento
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
