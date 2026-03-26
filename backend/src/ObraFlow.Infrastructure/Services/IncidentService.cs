using Microsoft.EntityFrameworkCore;
using ObraFlow.Application.Abstractions;
using ObraFlow.Application.DTOs.Incidents;
using ObraFlow.Domain.Entities;
using ObraFlow.Infrastructure.Persistence;

namespace ObraFlow.Infrastructure.Services;

public class IncidentService : IIncidentService
{
    private readonly AppDbContext _dbContext;

    public IncidentService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<IncidentDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Incidents
            .AsNoTracking()
            .OrderByDescending(x => x.ReportedAtUtc)
            .Select(x => ToDto(x))
            .ToListAsync(cancellationToken);
    }

    public async Task<IncidentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Incidents
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => ToDto(x))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IncidentDto> CreateAsync(CreateIncidentDto dto, CancellationToken cancellationToken = default)
    {
        var incident = new Incident
        {
            Id = Guid.NewGuid(),
            Title = dto.Title.Trim(),
            Description = dto.Description.Trim(),
            Status = dto.Status,
            ReportedAtUtc = dto.ReportedAtUtc.ToUniversalTime()
        };

        _dbContext.Incidents.Add(incident);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ToDto(incident);
    }

    public async Task<IncidentDto?> UpdateAsync(Guid id, UpdateIncidentDto dto, CancellationToken cancellationToken = default)
    {
        var incident = await _dbContext.Incidents
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (incident is null)
        {
            return null;
        }

        incident.Title = dto.Title.Trim();
        incident.Description = dto.Description.Trim();
        incident.Status = dto.Status;
        incident.ReportedAtUtc = dto.ReportedAtUtc.ToUniversalTime();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return ToDto(incident);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var incident = await _dbContext.Incidents
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (incident is null)
        {
            return false;
        }

        _dbContext.Incidents.Remove(incident);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static IncidentDto ToDto(Incident incident)
    {
        return new IncidentDto
        {
            Id = incident.Id,
            Title = incident.Title,
            Description = incident.Description,
            Status = incident.Status,
            ReportedAtUtc = DateTime.SpecifyKind(incident.ReportedAtUtc, DateTimeKind.Utc)
        };
    }
}
