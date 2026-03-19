namespace ObraFlow.Domain.Entities;

public class Worker
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public ICollection<DailyReport> DailyReports { get; set; } = new List<DailyReport>();
}
