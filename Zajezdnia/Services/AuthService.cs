using Zajezdnia.DTOs;
using Zajezdnia.Models;
using Zajezdnia.Repositories;
using Zajezdnia.Services;

namespace Zajezdnia.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    

    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        
        if (await _userRepository.ExistsAsync(dto.Username))
            return false;

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };
        
        if (dto.Password != dto.ConfirmPassword)
            return false;
        
        await _userRepository.AddAsync(user);
        return true;
    }

    public async Task<User?> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user == null) return null;

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return null;

        return user;
    }
}