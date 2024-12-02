using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_ACME.Models
{
    public class SurveyField
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsRequired { get; set; }

        [Required]
        public string FieldType { get; set; } // Texto, Número, Fecha

        // Relación con la encuesta
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }

        public Survey Survey { get; set; }
    }
}
