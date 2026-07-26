using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IJwtToken
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>JWT token</returns>
    string Generate(User user);
}
