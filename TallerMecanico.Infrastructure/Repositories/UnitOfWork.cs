using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TallerMecanico.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TallerMecanicoContext _context;
        private readonly IDapperContext _dapper;
        private IDbContextTransaction _transaction;

        public IBaseRepository<Servicio> ServicioRepository { get; }
        public IBaseRepository<OrdenTrabajo> OrdenTrabajoRepository { get; }
        public IBaseRepository<Vehiculo> VehiculoRepository { get; }

        public UnitOfWork(TallerMecanicoContext context, IDapperContext dapper)
        {
            _context = context;
            _dapper = dapper;

            ServicioRepository = new BaseRepository<Servicio>(_context);
            OrdenTrabajoRepository = new BaseRepository<OrdenTrabajo>(_context);
            VehiculoRepository = new BaseRepository<Vehiculo>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            var conn = _context.Database.GetDbConnection();
            var tx = _transaction.GetDbTransaction();
            _dapper.SetAmbientConnection(conn, tx);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
            _dapper.ClearAmbientConnection();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            _dapper.ClearAmbientConnection();
        }

        public IDbConnection GetDbConnection()
        {
            return _context.Database.GetDbConnection();
        }

        public IDbTransaction GetDbTransaction()
        {
            return _transaction.GetDbTransaction();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}