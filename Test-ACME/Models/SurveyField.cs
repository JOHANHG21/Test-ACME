using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_ACME.Models
{
    public class SurveyField
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del campo es obligatorio.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El título del campo es obligatorio.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "El tipo del campo es obligatorio.")]
        public string FieldType { get; set; } // Tipo de campo (Texto, Número, Fecha)
        public bool IsRequired { get; set; } // Si el campo es requerido o no
        [ValidateNever]
        public Survey Survey { get; set; } // Relación con la encuesta
        public int SurveyId { get; set; }

        // Relación de uno a muchos con SurveyResponses
        public ICollection<SurveyResponse> SurveyResponses { get; set; } = new List<SurveyResponse>();
    }
}
