using ObraFlow.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObraFlow.Domain.Entities;

namespace ObraFlow.Infrastructure.Persistence.Configurations;

public class DailyReportConfiguration : IEntityTypeConfiguration<DailyReport>
{
    public void Configure(EntityTypeBuilder<DailyReport> builder)
    {
        builder.ToTable("dailyReports");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Date)
            .IsRequired();
        
        builder.Property(x => x.HoursWorked)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasData(DailyReportSeed.Data);
    }
}
