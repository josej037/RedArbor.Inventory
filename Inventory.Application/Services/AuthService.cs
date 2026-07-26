using Inventory.Application.DTOs.Auth;
using Inventory.Application.Interfaces;
using Inventory.Application.Services.Interfaces;

namespace Inventory.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly IJwtToken _jwtToken;

    public AuthService(IUserRepository repository, IJwtToken jwtToken)
    {
        _repository = repository;
        _jwtToken = jwtToken;
    }

    /// <summary>
    /// Login user and generate JWT token
    /// </summary>
    /// <param name="request"></param>
    /// <returns>LoginResponse</returns>
    public async Task<LoginResponse?> Login(LoginRequest request)
    {
        var user = await _repository.Login(request.Username);
        if (user is null)
            return null;
        if (user.Password != request.Password)
            return null;
        var token = _jwtToken.Generate(user);
        return new LoginResponse
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddMinutes(60)
        };
    }
}
