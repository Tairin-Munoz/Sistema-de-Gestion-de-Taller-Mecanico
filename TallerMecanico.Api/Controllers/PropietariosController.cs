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
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetAllAsync();
        var dto = _mapper.Map<IEnumerable<PropietarioDto>>(data);
        return Ok(new ApiResponse<IEnumerable<PropietarioDto>>(dto));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();

        var dto = _mapper.Map<PropietarioDto>(item);
        return Ok(new ApiResponse<PropietarioDto>(dto));
    }

    [HttpPost]
    public async Task<IActionResult> Post(PropietarioDto dto)
    {
        var val = await _crearValidator.ValidateAsync(dto);
        if (!val.IsValid) return BadRequest(val.Errors);

        var entity = _mapper.Map<Propietario>(dto);
        await _service.Insert(entity);

        return Ok(new ApiResponse<PropietarioDto>(dto));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PropietarioDto dto)
    {
        if (id != dto.Id) return BadRequest();

        var val = await _actualizarValidator.ValidateAsync(dto);
        if (!val.IsValid) return BadRequest(val.Errors);

        var entity = _mapper.Map<Propietario>(dto);
        await _service.Update(entity);

        return Ok(new ApiResponse<bool>(true));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}