using Inventory.Application.Auth.Queries.GetLogin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly IMediator _auth;

    public AuthController(IMediator auth)
    {
        _auth = auth;
    }

    /// <summary>
    /// This API allows a user to log in to the application.
    /// </summary>
    /// <returns>An authentication token is returned that can be used to access protected resources.</returns>
    /// <response code="200">Login successful</response>
    /// <response code="401">Invalid credentials</response>   
    /// <response code="500">An error occurred while logging in.</response>   
    [HttpPost]
    public async Task<IActionResult> Login(GetLoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _auth.Send(new GetLoginQuery(request), cancellationToken);

        if (!result.IsSuccess)
            return Unauthorized(result.Error);

        return Ok(result.Value);


    }
}
