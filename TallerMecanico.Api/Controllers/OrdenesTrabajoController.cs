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
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetAllDapperAsync();
        var dto = _mapper.Map<IEnumerable<OrdenTrabajoDto>>(data);

        return Ok(new ApiResponse<IEnumerable<OrdenTrabajoDto>>(dto));
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

        var entity = _mapper.Map<OrdenTrabajo>(dto);

        await _service.Insert(entity);

        return Ok(new ApiResponse<OrdenTrabajoDto>(dto));
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

        return Ok(new ApiResponse<OrdenTrabajoDto>(dto));
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