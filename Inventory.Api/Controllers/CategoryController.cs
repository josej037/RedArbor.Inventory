using Inventory.Application.DTOs.Category;
using Inventory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Inventory.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]"), Description("Manage categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _category;

    public CategoryController(ICategoryService category)
    {
        _category = category;
    }

    [HttpGet]
    [Description("Get all categories")]
    /// <response code="200">Returns the list of categories</response>
    public async Task<IActionResult> GetAll()
    {
        var categories = await _category.GetAll();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    [Description("Get a category by its ID")]
    /// <response code="200">Returns the requested category</response>
    /// <response code="404">Category not found</response>
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var category = await _category.GetById(id);
            if (category == null)
                return NotFound(new { message = "Category not found" });
            return Ok(category);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while getting the category.",
                error = ex.Message
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryRequest request)
    {
        try
        {
            var category = await _category.Create(request);
            return StatusCode(201, category);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while creating the category.",
                error = ex.Message
            });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CategoryRequest request)
    {
        try
        {
            await _category.Update(id, request);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while updating the category.",
                error = ex.Message
            });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _category.Delete(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while deleting the category.",
                error = ex.Message
            });
        }
    }

}
