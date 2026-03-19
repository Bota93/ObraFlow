using ObraFlow.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObraFlow.Domain.Entities;

namespace ObraFlow.Infrastructure.Persistence.Configurations;

public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.ToTable("workers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(140)
            .IsRequired();
        
        builder.Property(x => x.Role)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.HourlyRate)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasMany(x => x.DailyReports)
            .WithOne(x => x.Worker)
            .HasForeignKey(x => x.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(WorkerSeed.Data);
    }
}
