using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ObraFlow.Infrastructure.Persistence;
using ObraFlow.Infrastructure.Persistence.Seed;

namespace ObraFlow.Api.IntegrationTests.Operations;

public class DemoDatabaseResetTests
{
    [Fact]
    public async Task ResetAsync_ShouldRestoreDeterministicSeededState()
    {
        using var factory = new CustomWebApplicationFactory();

        DemoDatabaseResetResult firstResetResult;
        DemoDatabaseResetResult secondResetResult;

        await using (var firstScope = factory.Services.CreateAsyncScope())
        {
            var resetService = firstScope.ServiceProvider.GetRequiredService<DemoDatabaseResetService>();
            firstResetResult = await resetService.ResetAsync();
        }

        firstResetResult.WorkersCount.Should().Be(WorkerSeed.Data.Length);
        firstResetResult.DailyReportsCount.Should().Be(DailyReportSeed.Data.Length);
        firstResetResult.IncidentsCount.Should().Be(IncidentSeed.Data.Length);

        await using (var secondScope = factory.Services.CreateAsyncScope())
        {
            var resetService = secondScope.ServiceProvider.GetRequiredService<DemoDatabaseResetService>();
            secondResetResult = await resetService.ResetAsync();
        }

        secondResetResult.Should().Be(firstResetResult);

        await using var verificationScope = factory.Services.CreateAsyncScope();
        var dbContext = verificationScope.ServiceProvider.GetRequiredService<AppDbContext>();

        var workersCount = await dbContext.Workers.CountAsync();
        var dailyReportsCount = await dbContext.DailyReports.CountAsync();
        var incidentsCount = await dbContext.Incidents.CountAsync();

        workersCount.Should().Be(firstResetResult.WorkersCount);
        dailyReportsCount.Should().Be(firstResetResult.DailyReportsCount);
        incidentsCount.Should().Be(firstResetResult.IncidentsCount);

        (await dbContext.Workers.AnyAsync(worker => worker.Name == "Miguel Torres")).Should().BeTrue();
        (await dbContext.DailyReports.AnyAsync(report => report.Description.Contains("Coordinated subcontractors"))).Should().BeTrue();
        (await dbContext.Incidents.AnyAsync(incident => incident.Title == "Water ingress in basement corridor")).Should().BeTrue();
    }

    [Fact]
    public void ResetDemoMigrationPath_ShouldIncludeDashboardSeedDataMigration()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=obraflowdb;Username=postgres;Password=postgres")
            .Options;

        using var dbContext = new AppDbContext(options);

        var migrations = dbContext.Database.GetMigrations();

        migrations.Should().Contain("20260321170000_DashboardSeedData");
    }
}
