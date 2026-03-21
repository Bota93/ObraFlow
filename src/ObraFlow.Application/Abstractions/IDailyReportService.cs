using ObraFlow.Application.DTOs.DailyReports;

namespace ObraFlow.Application.Abstractions;

public interface IDailyReportService
{
    Task<IReadOnlyList<DailyReportDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DailyReportDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<DailyReportDto> CreateAsync(CreateDailyReportDto dto, CancellationToken cancellationToken = default);
    Task<DailyReportDto?> UpdateAsync(Guid id, UpdateDailyReportDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
