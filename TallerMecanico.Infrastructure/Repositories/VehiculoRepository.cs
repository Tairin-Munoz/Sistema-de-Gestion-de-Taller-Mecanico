using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;

namespace TallerMecanico.Infrastructure.Repositories;

public class VehiculoRepository : IVehiculoRepository
{
    private static List<Vehiculo> vehiculos = new();

    public List<Vehiculo> GetAll() => vehiculos;

    public Vehiculo GetById(int id) =>
        vehiculos.FirstOrDefault(v => v.Id == id);

    public void Add(Vehiculo vehiculo)
    {
        vehiculos.Add(vehiculo);
    }
}
