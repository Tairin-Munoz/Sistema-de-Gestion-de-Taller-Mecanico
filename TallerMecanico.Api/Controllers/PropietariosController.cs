using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FluentValidation;
using TallerMecanico.Api.Responses;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Core.Entities;
using TallerMecanico.Services.Interfaces;
using TallerMecanico.Services.Validators;

namespace TallerMecanico.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PropietariosController : ControllerBase
{
    private readonly IPropietarioService _service;
    private readonly IMapper _mapper;
    private readonly CrearPropietarioDtoValidator _crearValidator;
    private readonly ActualizarPropietarioDtoValidator _actualizarValidator;

    public PropietariosController(
        IPropietarioService service,
        IMapper mapper,
        CrearPropietarioDtoValidator crearValidator,
        ActualizarPropietarioDtoValidator actualizarValidator)
    {
        _service = service;
        _mapper = mapper;
        _crearValidator = crearValidator;
        _actualizarValidator = actualizarValidator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] string? nombre,
        [FromQuery] string? ci)
    {
        var data = await _service.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(nombre))
            data = data.Where(x => x.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();

        if (!string.IsNullOrWhiteSpace(ci))
            data = data.Where(x => x.CI.Contains(ci, StringComparison.OrdinalIgnoreCase)).ToList();

        var dto = _mapper.Map<IEnumerable<PropietarioDto>>(data);
        return Ok(new ApiResponse<IEnumerable<PropietarioDto>>(dto, true, "Propietarios obtenidos"));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound(new ApiResponse<object>(null, false, "Propietario no encontrado"));

        var dto = _mapper.Map<PropietarioDto>(item);
        return Ok(new ApiResponse<PropietarioDto>(dto, true, "Propietario encontrado"));
    }

    [HttpPost]
    public async Task<IActionResult> Post(PropietarioDto dto)
    {
        await _crearValidator.ValidateAndThrowAsync(dto);

        var entity = _mapper.Map<Propietario>(dto);
        await _service.Insert(entity);

        return Created(string.Empty, new ApiResponse<PropietarioDto>(dto, true, "Propietario creado"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PropietarioDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new ApiResponse<object>(null, false, "El ID no coincide"));

        await _actualizarValidator.ValidateAndThrowAsync(dto);
        var entity = _mapper.Map<Propietario>(dto);
        await _service.Update(entity);

        return Ok(new ApiResponse<PropietarioDto>(dto, true, "Propietario actualizado"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}