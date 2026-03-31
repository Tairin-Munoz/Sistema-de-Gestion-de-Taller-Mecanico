using Microsoft.AspNetCore.Mvc;
using TallerMecanico.Services.Interfaces;
using TallerMecanico.Core.DTOs;
using AutoMapper;
using TallerMecanico.Core.Entities;
using TallerMecanico.Api.Responses;

namespace TallerMecanico.Api.Controllers;

[ApiController]
[Route("api/vehiculos")]
public class VehiculosController : ControllerBase
{
    private readonly IVehiculoService _service;
    private readonly IMapper _mapper;

    public VehiculosController(IVehiculoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetAll();
        var result = _mapper.Map<IEnumerable<VehiculoDto>>(data);

        return Ok(new ApiResponse<IEnumerable<VehiculoDto>>(result));
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vehiculo = await _service.GetById(id);

        if (vehiculo == null)
            return NotFound(new { message = "Vehiculo no encontrado" });

        var result = _mapper.Map<VehiculoDto>(vehiculo);

        return Ok(new ApiResponse<VehiculoDto>(result));
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VehiculoDto dto)
    {
        var entity = _mapper.Map<Vehiculo>(dto);

        await _service.Create(entity);

        var result = _mapper.Map<VehiculoDto>(entity);

        return Ok(new ApiResponse<VehiculoDto>(result));
    }

    // PUT
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] VehiculoDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new { message = "El ID no coincide" });

        var entity = _mapper.Map<Vehiculo>(dto);

        await _service.Update(entity);

        return Ok(new ApiResponse<bool>(true));
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);

        return Ok(new ApiResponse<bool>(true));
    }
}