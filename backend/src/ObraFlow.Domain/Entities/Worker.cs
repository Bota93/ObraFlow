namespace ObraFlow.Domain.Entities;

public class Worker
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<DailyReport> DailyReports { get; set; } = new List<DailyReport>();
}
