namespace ObraFlow.Application.DTOs.Dashboard;

public class DashboardSummaryDto
{
    public int TotalWorkers { get; set; }
    public int ActiveWorkers { get; set; }
    public int TotalDailyReports { get; set; }
    public int OpenIncidents { get; set; }
    public int InProgressIncidents { get; set; }
    public int ResolvedIncidents { get; set; }
    public decimal HoursWorkedToday { get; set; }
    public decimal HoursWorkedLast7Days { get; set; }
}