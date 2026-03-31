using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Services.Interfaces;

namespace TallerMecanico.Services.Services;

public class VehiculoService : IVehiculoService
{
    private readonly IVehiculoRepository _repository;

    public VehiculoService(IVehiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Vehiculo>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Vehiculo> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task Create(Vehiculo vehiculo)
    {
        // 🔥 REGLA DE NEGOCIO
        if (string.IsNullOrEmpty(vehiculo.Placa))
            throw new Exception("La placa es obligatoria");

        await _repository.Add(vehiculo);
    }

    public async Task Update(Vehiculo vehiculo)
    {
        await _repository.Update(vehiculo);
    }

    public async Task Delete(int id)
    {
        var vehiculo = await _repository.GetById(id);

        if (vehiculo == null)
            throw new Exception("Vehiculo no encontrado");

        await _repository.Delete(vehiculo);
    }
}