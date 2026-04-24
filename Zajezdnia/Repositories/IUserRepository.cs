namespace Zajezdnia.Repositories;
using Zajezdnia.Models;
public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    Task<bool> ExistsAsync(string email);
}