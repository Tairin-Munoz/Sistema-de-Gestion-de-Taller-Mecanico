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

    /// <summary>
    /// Recupera la lista de servicios del taller mecánico
    /// </summary>
    /// <remarks>
    /// Este método obtiene los servicios registrados en el sistema.
    /// Utiliza Dapper para consultas optimizadas (GET) y AutoMapper
    /// para convertir las entidades en DTOs.
    /// Si ocurre un error, se devuelve un estado HTTP 500.
    /// </remarks>
    /// <returns>
    /// Un <see cref="IActionResult"/> que contiene una lista de 
    /// <see cref="ServicioDto"/>.
    /// </returns>
    /// <response code="200">Retorna la lista de servicios</response>
    /// <response code="404">No existen servicios registrados</response>
    /// <response code="500">Error interno del servidor</response>
    [HttpGet]
    public async Task<IActionResult> Get(
    [FromQuery] string? nombre,
    [FromQuery] decimal? precio,
    [FromQuery] bool? activo)
    {
        var data = await _service.GetAllDapperAsync();

        if (!string.IsNullOrEmpty(nombre))
            data = data.Where(x => x.Nombre.ToLower().Contains(nombre.ToLower())).ToList();

        if (precio.HasValue)
            data = data.Where(x => x.Precio >= precio.Value).ToList();

        if (activo.HasValue)
            data = data.Where(x => x.Activo == activo.Value).ToList();

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



    /// <summary>
    /// Registra un nuevo servicio en el sistema
    /// </summary>
    /// <remarks>
    /// Este método permite crear un nuevo servicio validando los datos
    /// mediante FluentValidation y guardándolo en la base de datos
    /// usando Entity Framework Core.
    /// </remarks>
    /// <param name="dto">Datos del servicio a registrar</param>
    /// <returns>Servicio creado correctamente</returns>
    /// <response code="201">Servicio creado</response>
    /// <response code="400">Datos inválidos</response>
    /// <response code="500">Error interno</response>
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