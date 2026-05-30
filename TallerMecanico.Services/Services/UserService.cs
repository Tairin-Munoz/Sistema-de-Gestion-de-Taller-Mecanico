using System.Security.Authentication;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Services.Interfaces;

namespace TallerMecanico.Services.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordService _passwordService;

    public UserService(IUnitOfWork unitOfWork, IPasswordService passwordService)
    {
        _unitOfWork = unitOfWork;
        _passwordService = passwordService;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _unitOfWork.UserRepository.GetById(id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _unitOfWork.UserRepository.GetAll();
    }

    public async Task<User> RegisterAsync(RegisterDto dto)
    {
        var users = await _unitOfWork.UserRepository.GetAll();

        if (users.Any(u => u.Username == dto.Username))
            throw new Exception("El nombre de usuario ya está en uso");

        if (users.Any(u => u.Email == dto.Email))
            throw new Exception("El correo ya está en uso");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Role = dto.Role,
            IsActive = true,
            Password = _passwordService.Hash(dto.Password)
        };

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return user;
    }

    public async Task<User> LoginAsync(LoginDto dto)
    {
        var users = await _unitOfWork.UserRepository.GetAll();
        var user = users.FirstOrDefault(u => u.Username == dto.Username);

        if (user == null)
            throw new AuthenticationException("Usuario o contraseña inválidos");

        if (!_passwordService.Check(user.Password, dto.Password))
            throw new AuthenticationException("Usuario o contraseña inválidos");

        if (!user.IsActive)
            throw new AuthenticationException("El usuario no está activo");

        return user;
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
