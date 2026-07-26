using Inventory.Application.DTOs.InventoryExit;
using Inventory.Application.Services.Interfaces;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Inventory.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class InventoryExitController : ControllerBase
{
    private readonly IInventoryExitService _service;

    public InventoryExitController(IInventoryExitService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var exits = await _service.GetAll();
        return Ok(exits);
    }

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
