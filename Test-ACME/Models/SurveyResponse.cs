using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test_ACME.Models;

public class SurveyResponse
{
    public int Id { get; set; }

    [Required]
    public int? SurveyId { get; set; }

    public int? FieldId { get; set; } // Campo opcional

    [Required]
    public string Value { get; set; }

    [ForeignKey("SurveyId")]
    public Survey Survey { get; set; }

    [ForeignKey("FieldId")]
    public SurveyField Field { get; set; }
}

