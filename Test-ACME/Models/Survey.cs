using System.ComponentModel.DataAnnotations;

namespace Test_ACME.Models
{
    public class Survey
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string UniqueLink { get; set; }

        // Relación con los campos
        public ICollection<SurveyField> Fields { get; set; }
    }
}
