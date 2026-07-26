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

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="category"></param>
    /// <returns>CategoryResponse</returns>
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

    /// <summary>
    /// Delete a category.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task Delete(int id)
    {
        var category = await _repository.GetById(id);
        if (category is null)
            throw new Exception("Category not found");
        await _repository.Delete(id);
    }

    /// <summary>
    /// List all categories.
    /// </summary>
    /// <returns>IEnumerable<CategoryResponse></returns>
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

    /// <summary>
    /// Get a category by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>CategoryResponse</returns>
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

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
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
