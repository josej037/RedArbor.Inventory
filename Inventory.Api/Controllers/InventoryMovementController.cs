using Inventory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class InventoryMovementController : ControllerBase
{
    private readonly IInventoryMovementService _service;

    public InventoryMovementController(IInventoryMovementService service)
    {
        _service = service;
    }

    /// <summary>
    /// API endpoint to retrieve all inventory movements.
    /// 1. MovementType: 1 - Entry
    /// 2. MovementType: 2 - Exit
    /// </summary>
    /// <returns>Returns the list of inventory movements</returns>
    /// <response code="200">List of inventory movements</response>
    /// <response code="401">Unauthorized</response>
    [HttpGet]
    public async Task<IActionResult> GetAll(int movementType)
    {
        var exits = await _service.GetAllByMovementType(movementType);
        return Ok(exits);
    }
}
