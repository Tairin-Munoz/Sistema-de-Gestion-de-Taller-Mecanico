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
public class ServiciosController : ControllerBase
{
    private readonly IServicioService _service;
    private readonly IMapper _mapper;
    private readonly CrearServicioDtoValidator _crearValidator;
    private readonly ActualizarServicioDtoValidator _actualizarValidator;

    public ServiciosController(
        IServicioService service,
        IMapper mapper,
        CrearServicioDtoValidator crearValidator,
        ActualizarServicioDtoValidator actualizarValidator)
    {
        _service = service;
        _mapper = mapper;
        _crearValidator = crearValidator;
        _actualizarValidator = actualizarValidator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetAllDapperAsync();
        var dto = _mapper.Map<IEnumerable<ServicioDto>>(data);

        return Ok(new ApiResponse<IEnumerable<ServicioDto>>(dto));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var servicio = await _service.GetByIdAsync(id);

        if (servicio == null)
            return NotFound("Servicio no encontrado");

        var dto = _mapper.Map<ServicioDto>(servicio);

        return Ok(new ApiResponse<ServicioDto>(dto));
    }

    [HttpPost]
    public async Task<IActionResult> Post(ServicioDto dto)
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

        var entity = _mapper.Map<Servicio>(dto);

        await _service.Insert(entity);

        var result = _mapper.Map<ServicioDto>(entity);

        return Ok(new ApiResponse<ServicioDto>(result));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ServicioDto dto)
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

        var servicio = await _service.GetByIdAsync(id);

        if (servicio == null)
            return NotFound("Servicio no encontrado");

        _mapper.Map(dto, servicio);
        await _service.Update(servicio);

        return Ok(new ApiResponse<ServicioDto>(dto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var servicio = await _service.GetByIdAsync(id);

        if (servicio == null)
            return NotFound("Servicio no encontrado");

        await _service.Delete(id);

        return NoContent();
    }
}