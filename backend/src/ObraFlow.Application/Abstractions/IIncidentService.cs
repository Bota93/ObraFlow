using ObraFlow.Application.DTOs.Incidents;

namespace ObraFlow.Application.Abstractions;

public interface IIncidentService
{
    Task<IReadOnlyList<IncidentDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IncidentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IncidentDto> CreateAsync(CreateIncidentDto dto, CancellationToken cancellationToken = default);
    Task<IncidentDto?> UpdateAsync(Guid id, UpdateIncidentDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}