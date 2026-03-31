using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Services.Interfaces;

public interface IVehiculoService
{
    Task<IEnumerable<Vehiculo>> GetAll();
    Task<Vehiculo> GetById(int id);
    Task Create(Vehiculo vehiculo);
    Task Update(Vehiculo vehiculo);
    Task Delete(int id);
}
