using Inventory.Application.DTOs.Category;
using Inventory.Application.Interfaces;
using Inventory.Application.Services.Interfaces;
using Inventory.Domain.Entities;

namespace Inventory.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<CategoryResponse> Create(CategoryRequest request)
    {
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description,
        };

        category.Id = await _repository.Create(category);
          
        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Active = category.Active
        };
    }

    public async Task Delete(int id)
    {
        var category = await _repository.GetById(id);
        if (category is null)
            throw new Exception("Category not found");
        await _repository.Delete(id);
    }

    public async Task<IEnumerable<CategoryResponse>> GetAll()
    {
        var categories = await _repository.GetAll();
        return categories.Select(category => new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Active = category.Active
        });
    }

    public async Task<CategoryResponse?> GetById(int id)
    {
        var category = await _repository.GetById(id);
        if (category is null)
            return null;
        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Active = category.Active
        };
    }

    public async Task Update(int id, CategoryRequest request)
    {
        var category = await _repository.GetById(id);
        if (category is null)
            throw new Exception("Category not found");
        category.Name = request.Name;
        category.Description = request.Description;
        category.UpdatedAt = DateTime.UtcNow;

        await _repository.Update(category);
    }
}
