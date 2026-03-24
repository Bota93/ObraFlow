using Microsoft.EntityFrameworkCore;
using ObraFlow.Infrastructure.Persistence.Seed;

namespace ObraFlow.Infrastructure.Persistence;

public sealed class DemoDatabaseResetService
{
    private readonly AppDbContext _dbContext;

    public DemoDatabaseResetService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DemoDatabaseResetResult> ResetAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.EnsureDeletedAsync(cancellationToken);

        var providerName = _dbContext.Database.ProviderName ?? string.Empty;
        var usesPostgreSql = providerName.Contains("Npgsql", StringComparison.OrdinalIgnoreCase);

        if (usesPostgreSql)
        {
            await _dbContext.Database.MigrateAsync(cancellationToken);
        }
        else
        {
            await _dbContext.Database.EnsureCreatedAsync(cancellationToken);
        }

        return await VerifySeedDataAsync(cancellationToken);
    }

    public async Task<DemoDatabaseResetResult> VerifySeedDataAsync(CancellationToken cancellationToken = default)
    {
        var result = new DemoDatabaseResetResult(
            WorkersCount: await _dbContext.Workers.CountAsync(cancellationToken),
            DailyReportsCount: await _dbContext.DailyReports.CountAsync(cancellationToken),
            IncidentsCount: await _dbContext.Incidents.CountAsync(cancellationToken));

        if (result.WorkersCount != WorkerSeed.Data.Length)
        {
            throw new InvalidOperationException(
                $"Demo seed verification failed for workers. Expected {WorkerSeed.Data.Length}, found {result.WorkersCount}.");
        }

        if (result.DailyReportsCount != DailyReportSeed.Data.Length)
        {
            throw new InvalidOperationException(
                $"Demo seed verification failed for daily reports. Expected {DailyReportSeed.Data.Length}, found {result.DailyReportsCount}.");
        }

        if (result.IncidentsCount != IncidentSeed.Data.Length)
        {
            throw new InvalidOperationException(
                $"Demo seed verification failed for incidents. Expected {IncidentSeed.Data.Length}, found {result.IncidentsCount}.");
        }

        return result;
    }
}

public sealed record DemoDatabaseResetResult(
    int WorkersCount,
    int DailyReportsCount,
    int IncidentsCount);
