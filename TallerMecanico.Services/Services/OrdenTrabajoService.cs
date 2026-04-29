using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Services.Interfaces;
using TallerMecanico.Infrastructure.Queries;

namespace TallerMecanico.Services.Services;

public class OrdenTrabajoService : IOrdenTrabajoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDapperContext _dapper;

    public OrdenTrabajoService(IUnitOfWork unitOfWork, IDapperContext dapper)
    {
        _unitOfWork = unitOfWork;
        _dapper = dapper;
    }

    public async Task<IEnumerable<OrdenTrabajo>> GetAllDapperAsync()
    {
        var sql = OrdenTrabajoQuery.GetAll;
        return await _dapper.QueryAsync<OrdenTrabajo>(sql);
    }

    public async Task<OrdenTrabajo> GetByIdAsync(int id)
    {
        return await _unitOfWork.OrdenTrabajoRepository.GetById(id);
    }

    public async Task Insert(OrdenTrabajo orden)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var vehiculo = await _unitOfWork.VehiculoRepository.GetById(orden.VehiculoId);
            if (vehiculo == null)
                throw new Exception("Vehículo no existe");

            await _unitOfWork.OrdenTrabajoRepository.Add(orden);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task Update(OrdenTrabajo orden)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _unitOfWork.OrdenTrabajoRepository.Update(orden);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task Delete(int id)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            await _unitOfWork.OrdenTrabajoRepository.Delete(id);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}