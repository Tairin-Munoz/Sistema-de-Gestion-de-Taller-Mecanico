using Microsoft.EntityFrameworkCore;
using TallerMecanico.Core.Entities;
using System.Reflection;

namespace TallerMecanico.Infrastructure.Data
{
    public class TallerMecanicoContext : DbContext
    {
        public TallerMecanicoContext(DbContextOptions<TallerMecanicoContext> options)
            : base(options)
        {
        }

        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Propietario> Propietarios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<OrdenTrabajo> OrdenesTrabajo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}