using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Services.Interfaces;

namespace TallerMecanico.Services.Services;

public class ServicioService : IServicioService
{
    private readonly IUnitOfWork _unitOfWork;

    public ServicioService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Servicio>> GetAllAsync()
    {
        return await _unitOfWork.ServicioRepository.GetAll();
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
            _unitOfWork.ServicioRepository.Update(servicio);

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