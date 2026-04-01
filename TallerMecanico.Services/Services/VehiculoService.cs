using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Services.Interfaces;

namespace TallerMecanico.Services.Services;

public class VehiculoService : IVehiculoService
{
    public readonly IBaseRepository<Vehiculo> _vehiculoRepository;
    public readonly IBaseRepository<Propietario> _propietarioRepository;

    public VehiculoService(
        IBaseRepository<Vehiculo> vehiculoRepository,
        IBaseRepository<Propietario> propietarioRepository)
    {
        _vehiculoRepository = vehiculoRepository;
        _propietarioRepository = propietarioRepository;
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
        // RN-01: Propietario debe existir
        var propietario = await _propietarioRepository.GetById(vehiculo.PropietarioId);
        if (propietario == null)
            throw new Exception("El propietario no existe");

        var vehiculos = await _vehiculoRepository.GetAll();

        // RN-02: Placa única
        if (vehiculos.Any(v => v.Placa == vehiculo.Placa))
            throw new Exception("La placa ya está registrada");

        // RN-03: No duplicar vehículo por propietario
        if (vehiculos.Any(v =>
            v.PropietarioId == vehiculo.PropietarioId &&
            v.Marca == vehiculo.Marca &&
            v.Modelo == vehiculo.Modelo))
        {
            throw new Exception("El propietario ya tiene un vehículo con la misma marca y modelo");
        }

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
}