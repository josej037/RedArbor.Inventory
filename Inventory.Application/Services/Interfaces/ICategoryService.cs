using Inventory.Application.DTOs.Category;
using Inventory.Domain.Entities;

namespace Inventory.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAll();

    Task<CategoryResponse?> GetById(int id);

    Task<CategoryResponse> Create(CategoryRequest request);

    Task Update(int id, CategoryRequest request);

    Task Delete(int id);

}
