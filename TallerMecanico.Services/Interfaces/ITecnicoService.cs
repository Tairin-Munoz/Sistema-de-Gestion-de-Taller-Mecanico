using System.Collections.Generic;
using System.Threading.Tasks;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Services.Interfaces;

public interface ITecnicoService
{
    Task<IEnumerable<Tecnico>> GetAllAsync();
    Task<Tecnico> GetByIdAsync(int id);
    Task Insert(Tecnico tecnico);
    Task Update(Tecnico tecnico);
    Task Delete(int id);
}
