using System.Data;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<Servicio> ServicioRepository { get; }
    IBaseRepository<OrdenTrabajo> OrdenTrabajoRepository { get; }
    IBaseRepository<Vehiculo> VehiculoRepository { get; }

    Task SaveChangesAsync();

    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();

    IDbConnection? GetDbConnection();
    IDbTransaction? GetDbTransaction();
}