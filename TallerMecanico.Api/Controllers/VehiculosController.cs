using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FluentValidation;
using TallerMecanico.Api.Responses;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Pagination;
using TallerMecanico.Core.QueryFilters;
using TallerMecanico.Services.Interfaces;
using TallerMecanico.Services.Validators;

namespace TallerMecanico.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class VehiculosController : ControllerBase
{
    private readonly IVehiculoService _service;
    private readonly IMapper _mapper;
    private readonly CrearVehiculoDtoValidator _crearValidator;
    private readonly ActualizarVehiculoDtoValidator _actualizarValidator;

    public VehiculosController(
        IVehiculoService service,
        IMapper mapper,
        CrearVehiculoDtoValidator crearValidator,
        ActualizarVehiculoDtoValidator actualizarValidator)
    {
        _service = service;
        _mapper = mapper;
        _crearValidator = crearValidator;
        _actualizarValidator = actualizarValidator;
    }

    /// <summary>
    /// Recupera la lista de vehículos registrados.
    /// </summary>
    /// <response code="200">Lista obtenida correctamente</response>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] VehiculoQueryFilter filter)
    {
        var data = await _service.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(filter.Marca))
            data = data.Where(x => x.Marca.Contains(filter.Marca, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filter.Modelo))
            data = data.Where(x => x.Modelo.Contains(filter.Modelo, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filter.Placa))
            data = data.Where(x => x.Placa.Contains(filter.Placa, StringComparison.OrdinalIgnoreCase));

        if (filter.PropietarioId.HasValue)
            data = data.Where(x => x.PropietarioId == filter.PropietarioId.Value);

        var dto = _mapper.Map<IEnumerable<VehiculoDto>>(data);
        var paged = PagedList<VehiculoDto>.Create(dto, filter.PageNumber, filter.PageSize);

        return Ok(new ApiResponse<PagedList<VehiculoDto>>(paged, true, "Vehículos obtenidos", null, paged.Pagination));
    }

    /// <summary>
    /// Obtiene un vehículo por identificador.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vehiculo = await _service.GetByIdAsync(id);

        if (vehiculo == null)
            return NotFound(new ApiResponse<object>(null, false, "Vehículo no encontrado"));

        var dto = _mapper.Map<VehiculoDto>(vehiculo);
        return Ok(new ApiResponse<VehiculoDto>(dto, true, "Vehículo encontrado"));
    }

    /// <summary>
    /// Registra un nuevo vehículo.
    /// </summary>
    /// <param name="dto">Datos del vehículo</param>
    /// <response code="201">Vehículo registrado</response>
    [HttpPost]
    public async Task<IActionResult> Post(VehiculoDto dto)
    {
        await _crearValidator.ValidateAndThrowAsync(dto);

        var entity = _mapper.Map<Vehiculo>(dto);
        await _service.Insert(entity);

        return Created(string.Empty, new ApiResponse<VehiculoDto>(dto, true, "Vehículo creado"));
    }

    /// <summary>
    /// Actualiza un vehículo existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, VehiculoDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new ApiResponse<object>(null, false, "El ID no coincide"));

        await _actualizarValidator.ValidateAndThrowAsync(dto);

        var vehiculo = await _service.GetByIdAsync(id);
        if (vehiculo == null)
            return NotFound(new ApiResponse<object>(null, false, "Vehículo no encontrado"));

        _mapper.Map(dto, vehiculo);
        await _service.Update(vehiculo);

        return Ok(new ApiResponse<VehiculoDto>(dto, true, "Vehículo actualizado"));
    }
    /// <summary>
    /// Elimina un vehículo.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var vehiculo = await _service.GetByIdAsync(id);

        if (vehiculo == null)
            return NotFound(new ApiResponse<object>(null, false, "Vehículo no encontrado"));

        await _service.Delete(id);
        return NoContent();
    }
}