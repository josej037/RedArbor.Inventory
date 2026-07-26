using Inventory.Application.DTOs.Auth;
using Inventory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// This API allows a user to log in to the application.
    /// </summary>
    /// <returns>An authentication token is returned that can be used to access protected resources.</returns>
    /// <response code="200">Login successful</response>
    /// <response code="401">Invalid credentials</response>   
    /// <response code="500">An error occurred while logging in.</response>   
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var user = await _authService.Login(request);
            if (user == null)
                return Unauthorized();
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while logging in.",
                error = ex.Message
            });
        }
    }
}
