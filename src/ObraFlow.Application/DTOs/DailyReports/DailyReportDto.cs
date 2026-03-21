namespace ObraFlow.Application.DTOs.DailyReports;

public class DailyReportDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public Guid WorkerId { get; set; }
    public string WorkerName { get; set; } = string.Empty;
    public decimal HoursWorked { get; set; }
    public string Description { get; set; } = string.Empty;
}