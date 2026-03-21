using System.ComponentModel.DataAnnotations;

namespace ObraFlow.Application.DTOs.DailyReports;

public class CreateDailyReportDto
{
    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public Guid WorkerId { get; set; }

    [Range(typeof(decimal), "0.01", "24.00", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    public decimal HoursWorked { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 3)]
    public string Description { get; set; } = string.Empty;
}