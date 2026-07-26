using Inventory.Application.DTOs.InventoryEntry;
using Inventory.Application.Services.Interfaces;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Inventory.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class InventoryEntryController : ControllerBase
{
    private readonly IInventoryEntryService _service;

    public InventoryEntryController(IInventoryEntryService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var entries = await _service.GetAll();
        return Ok(entries);
    }

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
