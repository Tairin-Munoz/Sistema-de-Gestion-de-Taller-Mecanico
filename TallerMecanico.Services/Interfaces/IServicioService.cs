using TallerMecanico.Core.Entities;

namespace TallerMecanico.Services.Interfaces;

public interface IServicioService
{
    Task<IEnumerable<Servicio>> GetAllAsync();
    Task<Servicio> GetByIdAsync(int id);
    Task Insert(Servicio servicio);
    Task Update(Servicio servicio);
    Task Delete(int id);
}