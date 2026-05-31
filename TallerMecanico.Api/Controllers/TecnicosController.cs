using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
public class TecnicosController : ControllerBase
{
    private readonly ITecnicoService _service;
    private readonly IMapper _mapper;
    private readonly CrearTecnicoDtoValidator _crearValidator;
    private readonly ActualizarTecnicoDtoValidator _actualizarValidator;

    public TecnicosController(
        ITecnicoService service,
        IMapper mapper,
        CrearTecnicoDtoValidator crearValidator,
        ActualizarTecnicoDtoValidator actualizarValidator)
    {
        _service = service;
        _mapper = mapper;
        _crearValidator = crearValidator;
        _actualizarValidator = actualizarValidator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] TecnicoQueryFilter filter)
    {
        var items = await _service.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(filter.Nombre))
            items = items.Where(x => x.Nombre.Contains(filter.Nombre, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filter.Email))
            items = items.Where(x => x.Email.Contains(filter.Email, StringComparison.OrdinalIgnoreCase));

        var dtos = _mapper.Map<IEnumerable<TecnicoDto>>(items);
        var paged = PagedList<TecnicoDto>.Create(dtos, filter.PageNumber, filter.PageSize);

        return Ok(new ApiResponse<PagedList<TecnicoDto>>(paged, true, "Técnicos obtenidos"));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tecnico = await _service.GetByIdAsync(id);
        var dto = _mapper.Map<TecnicoDto>(tecnico);
        return Ok(new ApiResponse<TecnicoDto>(dto, true, "Técnico encontrado"));
    }

    [HttpPost]
    public async Task<IActionResult> Post(TecnicoDto dto)
    {
        await _crearValidator.ValidateAndThrowAsync(dto);

        var entity = _mapper.Map<Tecnico>(dto);
        await _service.Insert(entity);

        return Created(string.Empty, new ApiResponse<TecnicoDto>(dto, true, "Técnico creado"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TecnicoDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new ApiResponse<object>(null, false, "El id no coincide"));

        await _actualizarValidator.ValidateAndThrowAsync(dto);

        var tecnico = await _service.GetByIdAsync(id);
        _mapper.Map(dto, tecnico);
        await _service.Update(tecnico);

        return Ok(new ApiResponse<TecnicoDto>(dto, true, "Técnico actualizado"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}
