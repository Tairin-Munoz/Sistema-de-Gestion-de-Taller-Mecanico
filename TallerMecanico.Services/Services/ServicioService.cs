using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Services.Interfaces;
using TallerMecanico.Infrastructure.Queries;

namespace TallerMecanico.Services.Services;

public class ServicioService : IServicioService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDapperContext _dapper;

    public ServicioService(IUnitOfWork unitOfWork, IDapperContext dapper)
    {
        _unitOfWork = unitOfWork;
        _dapper = dapper;
    }

    public async Task<IEnumerable<Servicio>> GetAllAsync()
    {
        return await _unitOfWork.ServicioRepository.GetAll();
    }

    public async Task<IEnumerable<Servicio>> GetAllDapperAsync()
    {
        var sql = ServicioQuery.GetAll;
        return await _dapper.QueryAsync<Servicio>(sql);
    }

    public async Task<Servicio> GetByIdAsync(int id)
    {
        return await _unitOfWork.ServicioRepository.GetById(id);
    }

    public async Task Insert(Servicio servicio)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            await _unitOfWork.ServicioRepository.Add(servicio);
            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task Update(Servicio servicio)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            await _unitOfWork.ServicioRepository.Update(servicio);
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
            await _unitOfWork.ServicioRepository.Delete(id);
            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}