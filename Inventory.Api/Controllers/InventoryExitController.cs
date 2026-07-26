using Inventory.Application.DTOs.InventoryExit;
using Inventory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class InventoryExitController : ControllerBase
{
    private readonly IInventoryExitService _service;

    public InventoryExitController(IInventoryExitService service)
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
        var exits = await _service.GetAll();
        return Ok(exits);
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
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var exit = await _service.GetById(id);
            if (exit == null)
                return NotFound(new { message = "Inventory exit not found" });
            return Ok(exit);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while getting the inventory exit.",
                error = ex.Message
            });
        }
    }

    /// <summary>
    /// API endpoint creates a new inventory exit.
    /// </summary>
    /// <returns>Returns the requested inventory exit</returns>
    /// <response code="201">Inventory exit created</response>
    /// <response code="500">An error occurred while creating the inventory exit</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost]
    public async Task<IActionResult> Create(InventoryExitRequest request)
    {
        try
        {
            var exit = await _service.Create(request);
            return StatusCode(201, exit);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while creating the inventory exit.",
                error = ex.Message
            });
        }
    }

    /// <summary>
    /// API endpoint to retrieve an inventory exit by its ID.
    /// </summary>
    /// <returns>Returns the requested inventory exit</returns>
    /// <response code="200">Inventory exit Item</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while getting the inventory exit</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, InventoryExitRequest request)
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
                message = "An error occurred while updating the inventory exit.",
                error = ex.Message
            });
        }
    }

    /// <summary>
    /// API endpoint deletes an existing inventory exit.
    /// </summary>
    /// <returns>Returns the requested inventory exit</returns>
    /// <response code="200">Inventory exit deleted</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while deleting the inventory exit</response>
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
                message = "An error occurred while deleting the inventory exit.",
                error = ex.Message
            });
        }
    }

}
