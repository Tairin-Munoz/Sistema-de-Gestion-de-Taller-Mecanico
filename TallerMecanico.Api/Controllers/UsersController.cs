using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerMecanico.Api.Responses;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Services.Interfaces;

namespace TallerMecanico.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "CEO")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    /// <summary>
    /// Recupera la lista de usuarios registrados.
    /// </summary>
    /// <response code="200">Lista obtenida correctamente</response>
    /// <response code="404">No existen usuarios</response>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        var dtos = users.Select(u => _mapper.Map<UserDto>(u));
        return Ok(new ApiResponse<IEnumerable<UserDto>>(dtos, true, "Usuarios obtenidos"));
    }

    /// <summary>
    /// Obtiene un usuario por identificador.
    /// </summary>
    /// <param name="id">Id del usuario</param>
    /// <response code="200">Usuario encontrado</response>
    /// <response code="404">Usuario no encontrado</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        var dto = _mapper.Map<UserDto>(user);
        return Ok(new ApiResponse<UserDto>(dto, true, "Usuario encontrado"));
    }

    /// <summary>
    /// Elimina un usuario del sistema.
    /// </summary>
    /// <param name="id">Id del usuario</param>
    /// <response code="200">Usuario eliminado</response>
    /// <response code="404">Usuario no encontrado</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}
