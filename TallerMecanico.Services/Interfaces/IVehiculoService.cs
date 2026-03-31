using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Services.Interfaces;

public interface IVehiculoService
{
    Task<IEnumerable<Vehiculo>> GetAllAsync();
    Task<Vehiculo> GetByIdAsync(int id);
    Task Insert(Vehiculo vehiculo);
    Task Update(Vehiculo vehiculo);
    Task Delete(int id);
}
