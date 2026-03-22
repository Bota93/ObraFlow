namespace ObraFlow.Application.DTOs.Workers;

public class WorkerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public bool IsActive { get; set; }
}
