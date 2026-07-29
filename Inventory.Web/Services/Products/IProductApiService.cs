using Inventory.Web.Models.Products;

namespace Inventory.Web.Services.Products
{
    public interface IProductApiService
    {
        Task<List<ProductDto?>?> GetAll();
        Task<ProductDto?> GetById(int id);
        Task<ProductDto?> Create(ProductDto request);
        Task<bool> Update(int id, ProductDto request);
        Task<bool> Delete(int id);
    }
}
