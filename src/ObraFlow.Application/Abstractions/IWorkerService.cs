using ObraFlow.Application.DTOs.Workers;

namespace ObraFlow.Application.Abstractions;

public interface IWorkerService
{
    Task<IReadOnlyList<WorkerDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<WorkerDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<WorkerDto> CreateAsync(CreateWorkerDto dto, CancellationToken cancellationToken = default);
    Task<WorkerDto?> UpdateAsync(Guid id, UpdateWorkerDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
