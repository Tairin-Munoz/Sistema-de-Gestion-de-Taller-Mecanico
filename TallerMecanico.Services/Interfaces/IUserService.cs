using System.Collections.Generic;
using System.Threading.Tasks;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<User> RegisterAsync(RegisterDto dto);
    Task<User> LoginAsync(LoginDto dto);
    Task DeleteAsync(int id);
}
