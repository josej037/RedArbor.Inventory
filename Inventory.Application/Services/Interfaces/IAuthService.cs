using Inventory.Application.DTOs.Auth;

namespace Inventory.Application.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> Login(LoginRequest request);
}
