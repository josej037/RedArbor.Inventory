using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Verifies the user's credentials and returns a JWT token if valid.
    /// </summary>
    /// <param name="username"></param>
    /// <returns>User</returns>
    Task<User?> Login(string username);
}
