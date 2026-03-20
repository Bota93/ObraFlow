using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ObraFlow.Application.DTOs.Workers;

public class CreateWorkerDto
{
    [Required]
    [StringLength(140, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(80, MinimumLength = 2)]
    public string Role { get; set; } = string.Empty;

    [Required]
    [StringLength(30, MinimumLength = 7)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "999999.99", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    public decimal HourlyRate { get; set; }

    public bool IsActive { get; set; } = true;
}
