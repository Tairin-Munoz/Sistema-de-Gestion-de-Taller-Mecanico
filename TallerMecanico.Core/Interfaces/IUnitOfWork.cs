using System.Data;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<Servicio> ServicioRepository { get; }
    IBaseRepository<OrdenTrabajo> OrdenTrabajoRepository { get; }
    IBaseRepository<Vehiculo> VehiculoRepository { get; }
    IBaseRepository<Propietario> PropietarioRepository { get; }
    IBaseRepository<User> UserRepository { get; }
    IBaseRepository<Tecnico> TecnicoRepository { get; }

    Task SaveChangesAsync();

    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();

    IDbConnection? GetDbConnection();
    IDbTransaction? GetDbTransaction();
}