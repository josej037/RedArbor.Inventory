using Inventory.Application.InventoryExits.Commands.CreateInventoryExit;
using Inventory.Application.InventoryExits.Commands.DeleteInventoryExit;
using Inventory.Application.InventoryExits.Commands.UpdateInventoryExit;
using Inventory.Application.InventoryExits.DTOs;
using Inventory.Application.InventoryExits.Queries.GetInventoryExitById;
using Inventory.Application.InventoryExits.Queries.GetInventoryExits;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class InventoryExitController : ControllerBase
{
    private readonly IMediator _service;

    public InventoryExitController(IMediator service)
    {
        _service = service;
    }

    /// <summary>
    /// API endpoint to retrieve all inventory exits.
    /// </summary>
    /// <returns>Returns the list of inventory exits</returns>
    /// <response code="200">List of inventory exits</response>
    /// <response code="401">Unauthorized</response>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.Send(new GetInventoryExitsQuery());
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// API endpoint to retrieve an inventory exit by its ID.
    /// </summary>
    /// <returns>Returns the requested inventory exit</returns>
    /// <response code="200">Inventory exit Item</response>
    /// <response code="404">Inventory exit not found</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while getting the inventory exit</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _service.Send(new GetInventoryExitByIdQuery(id), cancellationToken);
        if (result.Value == null)
            return NotFound(new { message = "Inventory exit not found" });
        return Ok(result.Value);
    }

    /// <summary>
    /// API endpoint creates a new inventory exit.
    /// </summary>
    /// <returns>Returns the requested inventory exit</returns>
    /// <response code="201">Inventory exit created</response>
    /// <response code="500">An error occurred while creating the inventory exit</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost]
    public async Task<IActionResult> Create(InventoryExitDto request, CancellationToken cancellationToken)
    {
        var result = await _service.Send(new CreateInventoryExitCommand(request), cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// API endpoint to retrieve an inventory exit by its ID.
    /// </summary>
    /// <returns>Returns the requested inventory exit</returns>
    /// <response code="200">Inventory exit Item</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while getting the inventory exit</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, InventoryExitDto request, CancellationToken cancellationToken)
    {
        var result = await _service.Send(new UpdateInventoryExitCommand(id, request), cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// API endpoint deletes an existing inventory exit.
    /// </summary>
    /// <returns>Returns the requested inventory exit</returns>
    /// <response code="200">Inventory exit deleted</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while deleting the inventory exit</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _service.Send(new DeleteInventoryExitCommand(id), cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

}
