using Microsoft.AspNetCore.Mvc;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Core.DTOs;
using AutoMapper;
using TallerMecanico.Core.Entities;
using TallerMecanico.Api.Responses;

namespace TallerMecanico.Api.Controllers;

[ApiController]
[Route("api/vehiculos")]
public class VehiculosController : ControllerBase
{
    private readonly IVehiculoRepository _repo;
    private readonly IMapper _mapper;

    public VehiculosController(IVehiculoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _repo.GetAll();
        var result = _mapper.Map<IEnumerable<VehiculoDto>>(data);

        return Ok(new ApiResponse<IEnumerable<VehiculoDto>>(result));
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VehiculoDto dto)
    {
        var entity = _mapper.Map<Vehiculo>(dto);

        await _repo.Add(entity);

        var result = _mapper.Map<VehiculoDto>(entity);

        return Ok(new ApiResponse<VehiculoDto>(result));
    }
}