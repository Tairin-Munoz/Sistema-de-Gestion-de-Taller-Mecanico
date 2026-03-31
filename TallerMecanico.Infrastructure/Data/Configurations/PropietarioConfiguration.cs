using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Infrastructure.Data.Configurations;

public class PropietarioConfiguration : IEntityTypeConfiguration<Propietario>
{
    public void Configure(EntityTypeBuilder<Propietario> entity)
    {
        entity.HasKey(e => e.Id);

        entity.ToTable("propietario");

        entity.Property(e => e.Nombre)
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(e => e.Apellido)
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(e => e.CI)
            .HasMaxLength(20)
            .IsRequired();

        entity.Property(e => e.Telefono)
            .HasMaxLength(15)
            .IsRequired();
    }
}