using Inventory.Application.DTOs.Product;
using Inventory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/[controller]"), Description("Manage products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _product;

    public ProductController(IProductService product)
    {
        _product = product;
    }

    [HttpGet]
    [Description("Get all products")]
    /// <response code="200">Returns the list of products</response>
    public async Task<IActionResult> GetAll()
    {
        var products = await _product.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    [Description("Get a product by its ID")]
    /// <response code="200">Returns the requested product</response>
    /// <response code="404">Product not found</response>
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
