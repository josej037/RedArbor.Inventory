using Inventory.Application.DTOs.Product;
using Inventory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Inventory.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]"), Description("Manage products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _product;

    public ProductController(IProductService product)
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
        var products = await _product.GetAll();
        return Ok(products);
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
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var product = await _product.GetById(id);
            if (product == null)
                return NotFound(new { message = "Product not found" });
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while getting the product.",
                error = ex.Message
            });
        }
    }

    /// <summary>
    /// API endpoint creates a new product.
    /// </summary>
    /// <returns>Returns the requested product</returns>
    /// <response code="201">Product created</response>
    /// <response code="500">An error occurred while creating the product</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost]
    public async Task<IActionResult> Create(ProductRequest request)
    {
        try
        {
            var product = await _product.Create(request);
            return StatusCode(201, product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while creating the product.",
                error = ex.Message
            });
        }
    }

    /// <summary>
    /// API endpoint updates an existing product.
    /// </summary>
    /// <returns>Returns the requested product</returns>
    /// <response code="200">Product updated</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while updating the product</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProductRequest request)
    {
        try
        {
            await _product.Update(id, request);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while updating the product.",
                error = ex.Message
            });
        }
    }

    /// <summary>
    /// API endpoint deletes an existing product.
    /// </summary>
    /// <returns>Returns the requested product</returns>
    /// <response code="200">Product deleted</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">An error occurred while deleting the product</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _product.Delete(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while deleting the product.",
                error = ex.Message
            });
        }
    }
}
