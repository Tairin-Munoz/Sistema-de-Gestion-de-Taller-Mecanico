using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Infrastructure.Data.Configurations;

public class TecnicoConfiguration : IEntityTypeConfiguration<Tecnico>
{
    public void Configure(EntityTypeBuilder<Tecnico> builder)
    {
        builder.ToTable("Tecnicos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nombre)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Telefono)
            .HasMaxLength(20);
    }
}