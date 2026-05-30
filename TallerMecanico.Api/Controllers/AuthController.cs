using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TallerMecanico.Api.Responses;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Services.Interfaces;
using TallerMecanico.Services.Validators;

namespace TallerMecanico.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IPasswordService _passwordService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly LoginValidator _loginValidator;
    private readonly RegisterValidator _registerValidator;

    public AuthController(
        IUserService userService,
        IPasswordService passwordService,
        IMapper mapper,
        IConfiguration configuration,
        LoginValidator loginValidator,
        RegisterValidator registerValidator)
    {
        _userService = userService;
        _passwordService = passwordService;
        _mapper = mapper;
        _configuration = configuration;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        await _registerValidator.ValidateAndThrowAsync(dto);

        var user = await _userService.RegisterAsync(dto);
        var result = _mapper.Map<UserDto>(user);

        return Created(string.Empty, new ApiResponse<UserDto>(result, true, "Usuario registrado correctamente"));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        await _loginValidator.ValidateAndThrowAsync(dto);

        var user = await _userService.LoginAsync(dto);
        var token = GenerateJwtToken(user);
        var response = new
        {
            Token = token,
            User = _mapper.Map<UserDto>(user)
        };

        return Ok(new ApiResponse<object>(response, true, "Inicio de sesión correcto"));
    }

    private string GenerateJwtToken(Core.Entities.User user)
    {
        var jwtSection = _configuration.GetSection("Jwt");
        var secretKey = jwtSection.GetValue<string>("SecretKey") ?? string.Empty;
        var issuer = jwtSection.GetValue<string>("Issuer") ?? string.Empty;
        var audience = jwtSection.GetValue<string>("Audience") ?? string.Empty;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("UserId", user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
