using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Services.Interfaces;

public interface IPropietarioService
{
    Task<IEnumerable<Propietario>> GetAllAsync();
    Task<Propietario> GetByIdAsync(int id);
    Task Insert(Propietario propietario);
    Task Update(Propietario propietario);
    Task Delete(int id);
}
