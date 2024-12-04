using System.ComponentModel.DataAnnotations;

namespace Test_ACME.Models
{
    public class Survey
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la encuesta es obligatorio.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "La descripción de la encuesta es obligatoria.")]
        public string Description { get; set; }
        public string UniqueLink { get; set; }

        // Inicializa la colección para evitar valores nulos
        public List<SurveyField> Fields { get; set; } = new List<SurveyField>();
    }

}
