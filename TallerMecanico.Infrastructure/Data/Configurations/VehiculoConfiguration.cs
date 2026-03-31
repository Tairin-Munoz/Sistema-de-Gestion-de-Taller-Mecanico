using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Infrastructure.Data.Configurations;

public class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> entity)
    {
        entity.HasKey(e => e.Id);

        entity.ToTable("vehiculo");

        entity.Property(e => e.Marca)
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(e => e.Modelo)
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(e => e.Placa)
            .HasMaxLength(20)
            .IsRequired();

        entity.HasOne(v => v.Propietario)
            .WithMany(p => p.Vehiculos)
            .HasForeignKey(v => v.PropietarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
