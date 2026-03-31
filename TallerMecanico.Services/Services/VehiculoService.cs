using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerMecanico.Core.Entities;
using TallerMecanico.Services.Interfaces;
using TallerMecanico.Core.Interfaces;

namespace TallerMecanico.Services.Services;

public class VehiculoService : IVehiculoService
{
    public readonly IBaseRepository<Vehiculo> _vehiculoRepository;

    public VehiculoService(IBaseRepository<Vehiculo> vehiculoRepository)
    {
        _vehiculoRepository = vehiculoRepository;
    }

    public async Task<IEnumerable<Vehiculo>> GetAllAsync()
    {
        return await _vehiculoRepository.GetAll();
    }

    public async Task<Vehiculo> GetByIdAsync(int id)
    {
        return await _vehiculoRepository.GetById(id);
    }

    public async Task Insert(Vehiculo vehiculo)
    {
        
        if (string.IsNullOrWhiteSpace(vehiculo.Placa))
            throw new Exception("La placa es obligatoria");

        if (ContainsForbiddenWord(vehiculo.Marca))
            throw new Exception("Contenido no permitido en la marca");

        await _vehiculoRepository.Add(vehiculo);
    }

    public async Task Update(Vehiculo vehiculo)
    {
        await _vehiculoRepository.Update(vehiculo);
    }

    public async Task Delete(int id)
    {
        await _vehiculoRepository.Delete(id);
    }

    
    public readonly string[] ForbiddenWords =
    {
        "odio",
        "violencia",
        "ilegal",
        "robado"
    };

    public bool ContainsForbiddenWord(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        foreach (var word in ForbiddenWords)
        {
            if (text.Contains(word, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }
}