using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Infrastructure.Data;

namespace TallerMecanico.Infrastructure.Repositories;

public class VehiculoRepository : IVehiculoRepository
{
    private readonly AppDbContext _context;

    public VehiculoRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Vehiculo> GetAll()
    {
        return _context.Vehiculos.ToList();
    }

    public Vehiculo GetById(int id)
    {
        return _context.Vehiculos.Find(id);
    }

    public void Add(Vehiculo vehiculo)
    {
        _context.Vehiculos.Add(vehiculo);
        _context.SaveChanges();
    }
}
