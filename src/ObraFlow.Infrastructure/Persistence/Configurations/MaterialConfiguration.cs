using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObraFlow.Domain.Entities;

namespace ObraFlow.Infrastructure.Persistence.Configurations;

public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.ToTable("materials");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.Unit)
            .HasMaxLength(30)
            .IsRequired();
    }
}