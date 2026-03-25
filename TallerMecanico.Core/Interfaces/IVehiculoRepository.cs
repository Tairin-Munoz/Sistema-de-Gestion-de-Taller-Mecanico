using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Core.Interfaces;

public interface IVehiculoRepository
{
    List<Vehiculo> GetAll();
    Vehiculo GetById(int id);
    void Add(Vehiculo vehiculo);
}
