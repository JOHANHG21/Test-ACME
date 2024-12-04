using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test_ACME.Models;

public class SurveyResponse
{
    public int Id { get; set; }

    [Required]
    public string Value { get; set; }

    public SurveyField Field { get; set; } // Relación con SurveyField
    public int? FieldId { get; set; }
    public Survey Survey { get; set; } // Relación con Survey
    [Required]
    public int? SurveyId { get; set; }
}

