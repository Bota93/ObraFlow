using ObraFlow.Application.DTOs.Dashboard;

namespace ObraFlow.Application.Abstractions;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync(CancellationToken cancellationToken = default);
}