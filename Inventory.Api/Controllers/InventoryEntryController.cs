using Inventory.Application.DTOs.InventoryEntry;
using Inventory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class InventoryEntryController : ControllerBase
{
    private readonly IInventoryEntryService _service;
    public InventoryEntryController(IInventoryEntryService service)
    {
        _service = service;
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
        var entries = await _service.GetAll();
        return Ok(entries);
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
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var entry = await _service.GetById(id);
            if (entry == null)
                return NotFound(new { message = "Inventory entry not found" });
            return Ok(entry);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while getting the inventory entry.",
                error = ex.Message
            });
        }
    }

    /// <summary>
    /// API endpoint creates a new inventory entry.
    /// </summary>
    /// <returns>Returns the requested inventory entry</returns>
    /// <response code="201">Inventory entry created</response>
    /// <response code="500">An error occurred while creating the inventory entry</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost]
    public async Task<IActionResult> Create(InventoryEntryRequest request)
    {
        try
        {
            var entry = await _service.Create(request);
            return StatusCode(201, entry);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while creating the inventory entry.",
                error = ex.Message
            });
        }
    }

    /// <summary>
    /// API endpoint to retrieve an inventory entry by its ID.
    /// </summary>
    /// <returns>Returns the requested inventory entry</returns>
    /// <response code="200">Inventory entry Item</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while getting the inventory entry</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, InventoryEntryRequest request)
    {
        try
        {
            await _service.Update(id, request);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while updating the inventory entry.",
                error = ex.Message
            });
        }
    }

    /// <summary>
    /// API endpoint deletes an existing inventory entry.
    /// </summary>
    /// <returns>Returns the requested inventory entry</returns>
    /// <response code="200">Inventory entry deleted</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while deleting the inventory entry</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.Delete(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while deleting the inventory entry.",
                error = ex.Message
            });
        }
    }

}
