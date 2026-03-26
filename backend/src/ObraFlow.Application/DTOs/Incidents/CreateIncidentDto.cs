using System.ComponentModel.DataAnnotations;
using ObraFlow.Domain.Enums;

namespace ObraFlow.Application.DTOs.Incidents;

public class CreateIncidentDto : IValidatableObject
{
    [Required]
    [StringLength(150, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000, MinimumLength = 5)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [EnumDataType(typeof(IncidentStatus))]
    public IncidentStatus Status { get; set; } = IncidentStatus.Open;

    [Required]
    public DateTime ReportedAtUtc { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ReportedAtUtc == default)
        {
            yield return new ValidationResult("The ReportedAtUtc field is required.", [nameof(ReportedAtUtc)]);
            yield break;
        }

        if (ReportedAtUtc.Kind != DateTimeKind.Utc)
        {
            yield return new ValidationResult(
                "The ReportedAtUtc field must be sent in UTC using an explicit 'Z' suffix.",
                [nameof(ReportedAtUtc)]);
        }
    }
}
