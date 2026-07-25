using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAll();
    Task<Category?> GetById(int id);
    Task<int> Create(Category category);
    Task Update(Category category);
    Task Delete(int id);
}
