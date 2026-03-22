using System.ComponentModel.DataAnnotations;

namespace ObraFlow.Application.DTOs.DailyReports;

public class CreateDailyReportDto : IValidatableObject
{
    public DateOnly Date { get; set; }

    public Guid WorkerId { get; set; }

    [Range(typeof(decimal), "0.01", "24.00", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    public decimal HoursWorked { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 3)]
    public string Description { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Date == default)
        {
            yield return new ValidationResult("The Date field is required.", [nameof(Date)]);
        }

        if (WorkerId == Guid.Empty)
        {
            yield return new ValidationResult("The WorkerId field is required.", [nameof(WorkerId)]);
        }
    }
}
