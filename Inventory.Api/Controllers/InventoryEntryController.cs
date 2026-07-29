using Inventory.Application.InventoryEntries.Commands.CreateInventoryEntry;
using Inventory.Application.InventoryEntries.Commands.DeleteInventoryEntry;
using Inventory.Application.InventoryEntries.Commands.UpdateInventoryEntry;
using Inventory.Application.InventoryEntries.DTOs;
using Inventory.Application.InventoryEntries.Queries.GetInventoryEntries;
using Inventory.Application.InventoryEntries.Queries.GetInventoryEntryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class InventoryEntryController : ControllerBase
{
    private readonly IMediator _service;
    public InventoryEntryController(IMediator inventoryEntry)
    {
        _service = inventoryEntry;
    }

    /// <summary>
    /// API endpoint to retrieve all inventory entries.
    /// </summary>
    /// <returns>Returns the list of inventory entries</returns>
    /// <response code="200">List of inventory entries</response>
    /// <response code="401">Unauthorized</response>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.Send(new GetInventoryEntriesQuery());
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// API endpoint to retrieve an inventory entry by its ID.
    /// </summary>
    /// <returns>Returns the requested inventory entry</returns>
    /// <response code="200">Inventory entry Item</response>
    /// <response code="404">Inventory entry not found</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while getting the inventory entry</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _service.Send(new GetInventoryEntryByIdQuery(id), cancellationToken);
        if (result.Value == null)
            return NotFound(new { message = "Inventory entry not found" });
        return Ok(result.Value);
    }

    /// <summary>
    /// API endpoint creates a new inventory entry.
    /// </summary>
    /// <returns>Returns the requested inventory entry</returns>
    /// <response code="201">Inventory entry created</response>
    /// <response code="500">An error occurred while creating the inventory entry</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost]
    public async Task<IActionResult> Create(InventoryEntryDto request, CancellationToken cancellationToken)
    {
        var result = await _service.Send(new CreateInventoryEntryCommand(request), cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// API endpoint to retrieve an inventory entry by its ID.
    /// </summary>
    /// <returns>Returns the requested inventory entry</returns>
    /// <response code="200">Inventory entry Item</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while getting the inventory entry</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, InventoryEntryDto request, CancellationToken cancellationToken)
    {
        var result = await _service.Send(new UpdateInventoryEntryCommand(id, request), cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// API endpoint deletes an existing inventory entry.
    /// </summary>
    /// <returns>Returns the requested inventory entry</returns>
    /// <response code="200">Inventory entry deleted</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while deleting the inventory entry</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _service.Send(new DeleteInventoryEntryCommand(id), cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

}
