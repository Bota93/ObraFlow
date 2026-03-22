using Microsoft.EntityFrameworkCore;
using ObraFlow.Application.Abstractions;
using ObraFlow.Application.DTOs.Dashboard;
using ObraFlow.Domain.Enums;
using ObraFlow.Infrastructure.Persistence;

namespace ObraFlow.Infrastructure.Services;

public class DashboardService : IDashboardService
{
    private readonly AppDbContext _dbContext;

    public DashboardService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync(CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var last7DaysStart = today.AddDays(-6);

        var totalWorkers = await _dbContext.Workers.CountAsync(cancellationToken);
        var activeWorkers = await _dbContext.Workers.CountAsync(x => x.IsActive, cancellationToken);
        var totalDailyReports = await _dbContext.DailyReports.CountAsync(cancellationToken);
        var openIncidents = await _dbContext.Incidents.CountAsync(x => x.Status == IncidentStatus.Open, cancellationToken);
        var inProgressIncidents = await _dbContext.Incidents.CountAsync(x => x.Status == IncidentStatus.InProgress, cancellationToken);
        var resolvedIncidents = await _dbContext.Incidents.CountAsync(x => x.Status == IncidentStatus.Resolved, cancellationToken);
        var hoursWorkedToday = await _dbContext.DailyReports
            .Where(x => x.Date == today)
            .SumAsync(x => (decimal?)x.HoursWorked, cancellationToken);
        var hoursWorkedLast7Days = await _dbContext.DailyReports
            .Where(x => x.Date >= last7DaysStart && x.Date <= today)
            .SumAsync(x => (decimal?)x.HoursWorked, cancellationToken);

        return new DashboardSummaryDto
        {
            TotalWorkers = totalWorkers,
            ActiveWorkers = activeWorkers,
            TotalDailyReports = totalDailyReports,
            OpenIncidents = openIncidents,
            InProgressIncidents = inProgressIncidents,
            ResolvedIncidents = resolvedIncidents,
            HoursWorkedToday = hoursWorkedToday ?? 0m,
            HoursWorkedLast7Days = hoursWorkedLast7Days ?? 0m
        };
    }
}
