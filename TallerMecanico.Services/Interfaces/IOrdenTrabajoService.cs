using TallerMecanico.Core.Entities;

namespace TallerMecanico.Services.Interfaces
{
    public interface IOrdenTrabajoService
    {
        Task<IEnumerable<OrdenTrabajo>> GetAllDapperAsync();
        Task<OrdenTrabajo> GetByIdAsync(int id);
        Task Insert(OrdenTrabajo orden);
        Task Update(OrdenTrabajo orden);
        Task Delete(int id);
    }
}