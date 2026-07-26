using Inventory.Application.DTOs.Auth;
using Inventory.Application.DTOs.Category;
using Inventory.Application.Services.Interfaces;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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
