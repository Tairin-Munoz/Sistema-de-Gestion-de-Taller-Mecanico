using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Services.Interfaces;

namespace TallerMecanico.Services.Services;

public class TecnicoService : ITecnicoService
{
    private readonly IBaseRepository<Tecnico> _tecnicoRepository;

    public TecnicoService(IBaseRepository<Tecnico> tecnicoRepository)
    {
        _tecnicoRepository = tecnicoRepository;
    }

    public async Task<IEnumerable<Tecnico>> GetAllAsync()
    {
        return await _tecnicoRepository.GetAll();
    }

    public async Task<Tecnico> GetByIdAsync(int id)
    {
        return await _tecnicoRepository.GetById(id);
    }

    public async Task Insert(Tecnico tecnico)
    {
        var tecnicos = await _tecnicoRepository.GetAll();

        if (tecnicos.Any(t => t.Email == tecnico.Email))
            throw new Exception("El correo ya está registrado");

        if (tecnicos.Any(t => t.Telefono == tecnico.Telefono))
            throw new Exception("El teléfono ya está registrado");

        await _tecnicoRepository.Add(tecnico);
    }

    public async Task Update(Tecnico tecnico)
    {
        await _tecnicoRepository.Update(tecnico);
    }

    public async Task Delete(int id)
    {
        await _tecnicoRepository.Delete(id);
    }
}
