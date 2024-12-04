using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_ACME.Models
{
    public class SurveyField
    {
        public int Id { get; set; }
        public int SurveyId { get; set; } // Clave foránea que conecta con Survey
        [Required(ErrorMessage = "El nombre del campo es obligatorio.")]
        public string Name { get; set; } // Nombre del campo
        [Required(ErrorMessage = "El título del campo es obligatorio.")]
        public string Title { get; set; } // Título del campo
        [Required(ErrorMessage = "El tipo del campo es obligatorio.")]
        public string FieldType { get; set; } // Tipo de campo (Texto, Número, Fecha)
        public bool IsRequired { get; set; } // Si el campo es requerido o no
        [ValidateNever]
        public Survey Survey { get; set; } // Relación con la encuesta

        // Relación de uno a muchos con SurveyResponses
        public ICollection<SurveyResponse> SurveyResponses { get; set; } = new List<SurveyResponse>();
    }
}
