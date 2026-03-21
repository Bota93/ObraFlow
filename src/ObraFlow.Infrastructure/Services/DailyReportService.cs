using Microsoft.EntityFrameworkCore;
using ObraFlow.Application.Abstractions;
using ObraFlow.Application.DTOs.DailyReports;
using ObraFlow.Domain.Entities;
using ObraFlow.Infrastructure.Persistence;

namespace ObraFlow.Infrastructure.Services;

public class DailyReportService : IDailyReportService
{
    private readonly AppDbContext _dbContext;

    public DailyReportService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<DailyReportDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.DailyReports
            .AsNoTracking()
            .Include(x => x.Worker)
            .OrderByDescending(x => x.Date)
            .ThenBy(x => x.Worker!.Name)
            .Select(x => ToDto(x))
            .ToListAsync(cancellationToken);
    }

    public async Task<DailyReportDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.DailyReports
            .AsNoTracking()
            .Include(x => x.Worker)
            .Where(x => x.Id == id)
            .Select(x => ToDto(x))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<DailyReportDto?> CreateAsync(CreateDailyReportDto dto, CancellationToken cancellationToken = default)
    {
        var worker = await _dbContext.Workers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == dto.WorkerId, cancellationToken);

        if (worker is null)
        {
            return null;
        }

        var dailyReport = new DailyReport
        {
            Id = Guid.NewGuid(),
            Date = dto.Date,
            WorkerId = dto.WorkerId,
            HoursWorked = dto.HoursWorked,
            Description = dto.Description.Trim()
        };

        _dbContext.DailyReports.Add(dailyReport);
        await _dbContext.SaveChangesAsync(cancellationToken);

        dailyReport.Worker = worker;

        return ToDto(dailyReport);
    }

    public async Task<DailyReportDto?> UpdateAsync(Guid id, UpdateDailyReportDto dto, CancellationToken cancellationToken = default)
    {
        var dailyReport = await _dbContext.DailyReports
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (dailyReport is null)
        {
            return null;
        }

        var worker = await _dbContext.Workers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == dto.WorkerId, cancellationToken);

        if (worker is null)
        {
            return null;
        }

        dailyReport.Date = dto.Date;
        dailyReport.WorkerId = dto.WorkerId;
        dailyReport.HoursWorked = dto.HoursWorked;
        dailyReport.Description = dto.Description.Trim();

        await _dbContext.SaveChangesAsync(cancellationToken);

        dailyReport.Worker = worker;

        return ToDto(dailyReport);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dailyReport = await _dbContext.DailyReports
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (dailyReport is null)
        {
            return false;
        }

        _dbContext.DailyReports.Remove(dailyReport);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static DailyReportDto ToDto(DailyReport dailyReport)
    {
        return new DailyReportDto
        {
            Id = dailyReport.Id,
            Date = dailyReport.Date,
            WorkerId = dailyReport.WorkerId,
            WorkerName = dailyReport.Worker?.Name ?? string.Empty,
            HoursWorked = dailyReport.HoursWorked,
            Description = dailyReport.Description
        };
    }
}