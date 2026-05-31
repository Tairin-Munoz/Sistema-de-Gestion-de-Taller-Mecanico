using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Infrastructure.Data.Configurations;

public class OrdenTrabajoConfiguration : IEntityTypeConfiguration<OrdenTrabajo>
{
    public void Configure(EntityTypeBuilder<OrdenTrabajo> builder)
    {
        builder.ToTable("OrdenesTrabajo");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Estado)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasOne(x => x.Vehiculo)
            .WithMany()
            .HasForeignKey(x => x.VehiculoId);

        builder.HasOne(x => x.Servicio)
            .WithMany()
            .HasForeignKey(x => x.ServicioId);
    }
}