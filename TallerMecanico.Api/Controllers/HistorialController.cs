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
    /// <summary>
    /// Recupera el historial de servicios y órdenes de trabajo.
    /// </summary>
    /// <remarks>
    /// Permite consultar el historial mediante filtros
    /// por vehículo, placa, propietario,
    /// estado y rango de fechas.
    /// </remarks>
    /// <param name="filter">
    /// Filtros de búsqueda del historial.
    /// </param>
    /// <response code="200">
    /// Historial obtenido correctamente.
    /// </response>
    /// <response code="400">
    /// Parámetros de búsqueda inválidos.
    /// </response>
    /// <response code="500">
    /// Error interno del servidor.
    /// </response>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] HistorialQueryFilter filter)
    {
        var items = await _service.GetHistorialAsync(filter);
        var paged = PagedList<HistorialDto>.Create(items, filter.PageNumber, filter.PageSize);
        return Ok(new ApiResponse<PagedList<HistorialDto>>(paged, true, "Historial obtenido"));
    }
}
