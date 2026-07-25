using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(int id);
    Task<int> Create(Product product);
    Task Update(Product product);
    Task Delete(int id);
}
