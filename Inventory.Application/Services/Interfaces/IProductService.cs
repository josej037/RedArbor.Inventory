using Inventory.Application.DTOs.Product;
namespace Inventory.Application.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAll();
    Task<ProductResponse?> GetById(int id);
    Task<ProductResponse> Create(ProductRequest request);
    Task Update(int id, ProductRequest request);
    Task Delete(int id);
}
