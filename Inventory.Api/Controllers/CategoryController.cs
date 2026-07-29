using Inventory.Application.Categories.Commands.CreateCategory;
using Inventory.Application.Categories.Commands.DeleteCategory;
using Inventory.Application.Categories.Commands.UpdateCategory;
using Inventory.Application.Categories.Queries.GetCategories;
using Inventory.Application.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _category;

    public CategoryController(IMediator category)
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
        var categories = await _category.Send(new GetCategoriesQuery());
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
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var category = await _category.Send(
        new GetCategoryByIdQuery(id),
        cancellationToken);

        if (category is null)
            return NotFound();

        return Ok(category);
    }

    /// <summary>
    /// API endpoint creates a new category.
    /// </summary>
    /// <returns>Returns the requested category</returns>
    /// <response code="201">Category created</response>
    /// <response code="500">An error occurred while creating the category</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _category.Send(new CreateCategoryCommand(request), cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// API endpoint updates an existing category.
    /// </summary>
    /// <returns>Returns the requested category</returns>
    /// <response code="200">Category updated</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while updating the category</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _category.Send(new UpdateCategoryCommand(id, request), cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// API endpoint deletes an existing category.
    /// </summary>
    /// <returns>Returns the requested category</returns>
    /// <response code="200">Category deleted</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while deleting the category</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _category.Send(new DeleteCategoryCommand(id), cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
