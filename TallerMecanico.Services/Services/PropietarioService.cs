using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Services.Interfaces;

namespace TallerMecanico.Services.Services;

public class PropietarioService : IPropietarioService
{
    private readonly IBaseRepository<Propietario> _repo;

    public PropietarioService(IBaseRepository<Propietario> repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Propietario>> GetAllAsync()
        => await _repo.GetAll();

    public async Task<Propietario> GetByIdAsync(int id)
        => await _repo.GetById(id);

    public async Task Insert(Propietario propietario)
    {
        if (string.IsNullOrWhiteSpace(propietario.Nombre))
            throw new Exception("Nombre obligatorio");

        await _repo.Add(propietario);
    }

    public async Task Update(Propietario propietario)
        => await _repo.Update(propietario);

    public async Task Delete(int id)
        => await _repo.Delete(id);
}
