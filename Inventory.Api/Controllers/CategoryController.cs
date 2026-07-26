using Inventory.Application.DTOs.Category;
using Inventory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Inventory.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _category;

    public CategoryController(ICategoryService category)
    {
        _category = category;
    }

    /// <summary>
    /// API endpoint to retrieve all categories.
    /// </summary>
    /// <returns>Returns the list of categories</returns>
    /// <response code="200">List of categories</response>
    /// <response code="401">Unauthorized</response>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _category.GetAll();
        return Ok(categories);
    }

    /// <summary>
    /// API endpoint to retrieve a category by its ID.
    /// </summary>
    /// <returns>Returns the requested category</returns>
    /// <response code="200">Category Item</response>
    /// <response code="404">Category not found</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while getting the category</response>
    [HttpGet("{id}")]
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

    /// <summary>
    /// API endpoint creates a new category.
    /// </summary>
    /// <returns>Returns the requested category</returns>
    /// <response code="201">Category created</response>
    /// <response code="500">An error occurred while creating the category</response>
    /// <response code="401">Unauthorized</response>
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

    /// <summary>
    /// API endpoint updates an existing category.
    /// </summary>
    /// <returns>Returns the requested category</returns>
    /// <response code="200">Category updated</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while updating the category</response>
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

    /// <summary>
    /// API endpoint deletes an existing category.
    /// </summary>
    /// <returns>Returns the requested category</returns>
    /// <response code="200">Category deleted</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while deleting the category</response>
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
