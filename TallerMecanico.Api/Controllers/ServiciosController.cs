using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
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
    public async Task<IActionResult> Get([FromQuery] ServicioQueryFilter filter)
    {
        var data = await _service.GetAllDapperAsync();

        if (!string.IsNullOrWhiteSpace(filter.Nombre))
            data = data.Where(x => x.Nombre.Contains(filter.Nombre, StringComparison.OrdinalIgnoreCase)).ToList();

        if (filter.PrecioMin.HasValue)
            data = data.Where(x => x.Precio >= filter.PrecioMin.Value).ToList();

        if (filter.PrecioMax.HasValue)
            data = data.Where(x => x.Precio <= filter.PrecioMax.Value).ToList();

        if (filter.Activo.HasValue)
            data = data.Where(x => x.Activo == filter.Activo.Value).ToList();

        var dto = _mapper.Map<IEnumerable<ServicioDto>>(data);
        var paged = PagedList<ServicioDto>.Create(dto, filter.PageNumber, filter.PageSize);

        return Ok(new ApiResponse<PagedList<ServicioDto>>(paged, true, "Servicios obtenidos", null, paged.Pagination));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var servicio = await _service.GetByIdAsync(id);

        if (servicio == null)
            return NotFound(new ApiResponse<object>(null, false, "Servicio no encontrado"));

        var dto = _mapper.Map<ServicioDto>(servicio);

        return Ok(new ApiResponse<ServicioDto>(dto, true, "Servicio encontrado"));
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
        await _crearValidator.ValidateAndThrowAsync(dto);

        var entity = _mapper.Map<Servicio>(dto);
        await _service.Insert(entity);

        var result = _mapper.Map<ServicioDto>(entity);
        return Created(string.Empty, new ApiResponse<ServicioDto>(result, true, "Servicio creado"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ServicioDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new ApiResponse<object>(null, false, "El ID no coincide"));

        await _actualizarValidator.ValidateAndThrowAsync(dto);

        var servicio = await _service.GetByIdAsync(id);
        if (servicio == null)
            return NotFound(new ApiResponse<object>(null, false, "Servicio no encontrado"));

        _mapper.Map(dto, servicio);
        await _service.Update(servicio);

        return Ok(new ApiResponse<ServicioDto>(dto, true, "Servicio actualizado"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var servicio = await _service.GetByIdAsync(id);

        if (servicio == null)
            return NotFound(new ApiResponse<object>(null, false, "Servicio no encontrado"));

        await _service.Delete(id);

        return NoContent();
    }
}