using Microsoft.EntityFrameworkCore;
using ObraFlow.Domain.Entities;

namespace ObraFlow.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Worker> Workers => Set<Worker>();
    public DbSet<DailyReport> DailyReports => Set<DailyReport>();
    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<Material> Materials => Set<Material>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
