using Inventory.Application.Products.Commands.CreateProduct;
using Inventory.Application.Products.Commands.DeleteProduct;
using Inventory.Application.Products.Commands.UpdateProduct;
using Inventory.Application.Products.Queries.GetProductById;
using Inventory.Application.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Inventory.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]"), Description("Manage products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _product;

    public ProductController(IMediator product)
    {
        _product = product;
    }

    /// <summary>
    /// API endpoint to retrieve all products.
    /// </summary>
    /// <returns>Returns the list of products</returns>
    /// <response code="200">List of products</response>
    /// <response code="401">Unauthorized</response>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _product.Send(new GetProductsQuery());

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// API endpoint to retrieve a product by its ID.
    /// </summary>
    /// <returns>Returns the requested product</returns>
    /// <response code="200">Product Item</response>
    /// <response code="404">Product not found</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while getting the product</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await _product.Send(new GetProductByIdQuery(id), cancellationToken);
        if (product == null)
            return NotFound(new { message = "Product not found" });
        return Ok(product);
    }

    /// <summary>
    /// API endpoint creates a new product.
    /// </summary>
    /// <returns>Returns the requested product</returns>
    /// <response code="201">Product created</response>
    /// <response code="500">An error occurred while creating the product</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _product.Send(new CreateProductCommand(request), cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// API endpoint updates an existing product.
    /// </summary>
    /// <returns>Returns the requested product</returns>
    /// <response code="200">Product updated</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while updating the product</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _product.Send(new UpdateProductCommand(id, request), cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// API endpoint deletes an existing product.
    /// </summary>
    /// <returns>Returns the requested product</returns>
    /// <response code="200">Product deleted</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while deleting the product</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _product.Send(new DeleteProductCommand(id), cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
