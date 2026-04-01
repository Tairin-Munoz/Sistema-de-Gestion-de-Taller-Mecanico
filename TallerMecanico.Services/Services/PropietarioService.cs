using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Services.Interfaces;

namespace TallerMecanico.Services.Services;

public class PropietarioService : IPropietarioService
{
    public readonly IBaseRepository<Propietario> _propietarioRepository;

    public PropietarioService(IBaseRepository<Propietario> propietarioRepository)
    {
        _propietarioRepository = propietarioRepository;
    }

    public async Task<IEnumerable<Propietario>> GetAllAsync()
    {
        return await _propietarioRepository.GetAll();
    }

    public async Task<Propietario> GetByIdAsync(int id)
    {
        return await _propietarioRepository.GetById(id);
    }

    public async Task Insert(Propietario propietario)
    {
        var propietarios = await _propietarioRepository.GetAll();

        // RN-01: CI único
        if (propietarios.Any(p => p.CI == propietario.CI))
            throw new Exception("El CI ya está registrado");

        // RN-02: No duplicar propietario
        if (propietarios.Any(p =>
            p.Nombre == propietario.Nombre &&
            p.Apellido == propietario.Apellido))
        {
            throw new Exception("El propietario ya está registrado");
        }

        // RN-03: Teléfono único
        if (propietarios.Any(p => p.Telefono == propietario.Telefono))
            throw new Exception("El teléfono ya está registrado");

        await _propietarioRepository.Add(propietario);
    }

    public async Task Update(Propietario propietario)
    {
        await _propietarioRepository.Update(propietario);
    }

    public async Task Delete(int id)
    {
        await _propietarioRepository.Delete(id);
    }
}