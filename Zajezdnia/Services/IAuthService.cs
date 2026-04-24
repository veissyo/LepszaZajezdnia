using Zajezdnia.DTOs;
using Zajezdnia.Models;

namespace Zajezdnia.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto dto);
    Task<User?> LoginAsync(LoginDto dto);
}