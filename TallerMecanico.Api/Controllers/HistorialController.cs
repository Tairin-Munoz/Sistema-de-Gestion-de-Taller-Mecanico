using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerMecanico.Api.Responses;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Core.Pagination;
using TallerMecanico.Core.QueryFilters;
using TallerMecanico.Services.Interfaces;

namespace TallerMecanico.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class HistorialController : ControllerBase
{
    private readonly IHistorialService _service;

    public HistorialController(IHistorialService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] HistorialQueryFilter filter)
    {
        var items = await _service.GetHistorialAsync(filter);
        var paged = PagedList<HistorialDto>.Create(items, filter.PageNumber, filter.PageSize);
        return Ok(new ApiResponse<PagedList<HistorialDto>>(paged, true, "Historial obtenido"));
    }
}
