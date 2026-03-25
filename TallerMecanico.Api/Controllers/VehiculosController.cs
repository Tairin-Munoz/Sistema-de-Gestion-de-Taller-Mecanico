using Microsoft.AspNetCore.Mvc;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Core.DTOs;
using AutoMapper;
using TallerMecanico.Core.Entities;

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

    [HttpGet]
    public IActionResult Get()
    {
        var data = _repo.GetAll();
        var result = _mapper.Map<List<VehiculoDto>>(data);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Post([FromBody] VehiculoDto dto)
    {
        var entity = _mapper.Map<Vehiculo>(dto);
        _repo.Add(entity);
        return Ok();
    }
}