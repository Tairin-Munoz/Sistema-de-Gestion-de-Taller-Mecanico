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
public class OrdenesTrabajoController : ControllerBase
{
    private readonly IOrdenTrabajoService _service;
    private readonly IMapper _mapper;
    private readonly CrearOrdenTrabajoDtoValidator _crearValidator;

    public OrdenesTrabajoController(
        IOrdenTrabajoService service,
        IMapper mapper,
        CrearOrdenTrabajoDtoValidator crearValidator)
    {
        _service = service;
        _mapper = mapper;
        _crearValidator = crearValidator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] OrdenTrabajoQueryFilter filter)
    {
        var data = await _service.GetAllDapperAsync();

        if (filter.VehiculoId.HasValue)
            data = data.Where(x => x.VehiculoId == filter.VehiculoId.Value).ToList();

        if (filter.ServicioId.HasValue)
            data = data.Where(x => x.ServicioId == filter.ServicioId.Value).ToList();

        if (!string.IsNullOrWhiteSpace(filter.Estado))
            data = data.Where(x => x.Estado.Contains(filter.Estado, StringComparison.OrdinalIgnoreCase)).ToList();

        if (filter.FechaDesde.HasValue)
            data = data.Where(x => x.Fecha >= filter.FechaDesde.Value).ToList();

        if (filter.FechaHasta.HasValue)
            data = data.Where(x => x.Fecha <= filter.FechaHasta.Value).ToList();

        var dto = _mapper.Map<IEnumerable<OrdenTrabajoDto>>(data);
        var paged = PagedList<OrdenTrabajoDto>.Create(dto, filter.PageNumber, filter.PageSize);

        return Ok(new ApiResponse<PagedList<OrdenTrabajoDto>>(paged, true, "Órdenes obtenidas", null, paged.Pagination));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var orden = await _service.GetByIdAsync(id);

        if (orden == null)
            return NotFound("Orden no encontrada");

        var dto = _mapper.Map<OrdenTrabajoDto>(orden);

        return Ok(new ApiResponse<OrdenTrabajoDto>(dto));
    }

    [HttpPost]
    public async Task<IActionResult> Post(OrdenTrabajoDto dto)
    {
        await _crearValidator.ValidateAndThrowAsync(dto);

        var entity = _mapper.Map<OrdenTrabajo>(dto);
        await _service.Insert(entity);

        return Created(string.Empty, new ApiResponse<OrdenTrabajoDto>(dto, true, "Orden de trabajo creada"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, OrdenTrabajoDto dto)
    {
        if (id != dto.Id)
            return BadRequest("El ID no coincide");

        var orden = await _service.GetByIdAsync(id);

        if (orden == null)
            return NotFound("Orden no encontrada");

        _mapper.Map(dto, orden);

        await _service.Update(orden);

        return Ok(new ApiResponse<OrdenTrabajoDto>(dto, true, "Orden de trabajo actualizada"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var orden = await _service.GetByIdAsync(id);

        if (orden == null)
            return NotFound("Orden no encontrada");

        await _service.Delete(id);

        return NoContent();
    }
}