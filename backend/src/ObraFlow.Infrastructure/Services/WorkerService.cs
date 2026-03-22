using Microsoft.EntityFrameworkCore;
using ObraFlow.Application.Abstractions;
using ObraFlow.Application.DTOs.Workers;
using ObraFlow.Domain.Entities;
using ObraFlow.Infrastructure.Persistence;

namespace ObraFlow.Infrastructure.Services;

public class WorkerService : IWorkerService
{
    private readonly AppDbContext _dbContext;

    public WorkerService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<WorkerDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Workers
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => ToDto(x))
            .ToListAsync(cancellationToken);
    }

    public async Task<WorkerDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Workers
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => ToDto(x))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<WorkerDto> CreateAsync(CreateWorkerDto dto, CancellationToken cancellationToken = default)
    {
        var worker = new Worker
        {
            Id = Guid.NewGuid(),
            Name = dto.Name.Trim(),
            Role = dto.Role.Trim(),
            PhoneNumber = dto.PhoneNumber.Trim(),
            HourlyRate = dto.HourlyRate,
            CreatedAtUtc = DateTime.UtcNow,
            IsActive = dto.IsActive,
        };

        _dbContext.Workers.Add(worker);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ToDto(worker);
    }

    public async Task<WorkerDto?> UpdateAsync(Guid id, UpdateWorkerDto dto, CancellationToken cancellationToken = default)
    {
        var worker = await _dbContext.Workers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (worker is null)
        {
            return null;
        }

        worker.Name = dto.Name.Trim();
        worker.Role = dto.Role.Trim();
        worker.PhoneNumber = dto.PhoneNumber.Trim();
        worker.HourlyRate = dto.HourlyRate;
        worker.IsActive = dto.IsActive;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return ToDto(worker);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var worker = await _dbContext.Workers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (worker is null)
        {
            return false;
        }

        _dbContext.Workers.Remove(worker);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static WorkerDto ToDto(Worker worker)
    {
        return new WorkerDto
        {
            Id = worker.Id,
            Name = worker.Name,
            Role = worker.Role,
            PhoneNumber = worker.PhoneNumber,
            HourlyRate = worker.HourlyRate,
            CreatedAtUtc = worker.CreatedAtUtc,
            IsActive = worker.IsActive,
        };
    }
}
