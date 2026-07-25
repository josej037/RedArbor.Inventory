using Inventory.Application.DTOs.Product;
using Inventory.Application.Interfaces;
using Inventory.Application.Services.Interfaces;
using Inventory.Domain.Entities;

namespace Inventory.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse> Create(ProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            CategoryId = request.CategoryId
        };
        product.Id = await _repository.Create(product);
          
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId
        };
    }

    public async Task Delete(int id)
    {
        var product = await _repository.GetById(id);
        if (product is null)
            throw new Exception("Product not found");
        await _repository.Delete(id);
    }

    public async Task<IEnumerable<ProductResponse>> GetAll()
    {
        var products = await _repository.GetAll();
        return products.Select(product => new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId,
            Active = product.Active
        });
    }

    public async Task<ProductResponse?> GetById(int id)
    {
        var product = await _repository.GetById(id);
        if (product is null)
            return null;
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId,
            Active = product.Active
        };
    }

    public async Task Update(int id, ProductRequest request)
    {
        var product = await _repository.GetById(id);
        if (product is null)
            throw new Exception("Product not found");
        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.CategoryId = request.CategoryId;
        product.UpdatedAt = DateTime.UtcNow;

        await _repository.Update(product);
    }
}
