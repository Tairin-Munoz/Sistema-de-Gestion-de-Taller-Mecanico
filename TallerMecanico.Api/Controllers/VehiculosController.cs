using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TallerMecanico.Api.Responses;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Core.Entities;
using TallerMecanico.Services.Interfaces;
using TallerMecanico.Services.Validators;

namespace TallerMecanico.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
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

  
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var vehiculos = await _service.GetAllAsync();
        var vehiculosDto = _mapper.Map<IEnumerable<VehiculoDto>>(vehiculos);

        var response = new ApiResponse<IEnumerable<VehiculoDto>>(vehiculosDto);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vehiculo = await _service.GetByIdAsync(id);

        if (vehiculo == null)
            return NotFound("Vehículo no encontrado");

        var dto = _mapper.Map<VehiculoDto>(vehiculo);

        return Ok(new ApiResponse<VehiculoDto>(dto));
    }

    
    [HttpPost]
    public async Task<IActionResult> Post(VehiculoDto dto)
    {
        var validation = await _crearValidator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return BadRequest(new
            {
                message = "Error de validación",
                errors = validation.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    error = e.ErrorMessage
                })
            });
        }

        try
        {
            var entity = _mapper.Map<Vehiculo>(dto);
            await _service.Insert(entity);

            return Ok(new ApiResponse<VehiculoDto>(dto));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error al crear vehículo",
                error = ex.Message
            });
        }
    }

  
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, VehiculoDto dto)
    {
        if (id != dto.Id)
            return BadRequest("El ID no coincide");

        var validation = await _actualizarValidator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return BadRequest(new
            {
                message = "Error de validación",
                errors = validation.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    error = e.ErrorMessage
                })
            });
        }

        var vehiculo = await _service.GetByIdAsync(id);

        if (vehiculo == null)
            return NotFound("Vehículo no encontrado");

        try
        {
            _mapper.Map(dto, vehiculo);

            await _service.Update(vehiculo);

            return Ok(new ApiResponse<VehiculoDto>(dto));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error al actualizar",
                error = ex.Message
            });
        }
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var vehiculo = await _service.GetByIdAsync(id);

        if (vehiculo == null)
            return NotFound("Vehículo no encontrado");

        await _service.Delete(id);

        return NoContent();
    }
}