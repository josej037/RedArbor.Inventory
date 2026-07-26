using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface ICategoryRepository
{
    /// <summary>
    /// List all categories.
    /// </summary>
    /// <returns>IEnumerable<Category></returns>
    Task<IEnumerable<Category>> GetAll();

    /// <summary>
    /// Gets a category by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Category</returns>
    Task<Category?> GetById(int id);

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <returns>ID</returns>
    Task<int> Create(Category category);

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    Task Update(Category category);

    /// <summary>
    /// Deletes a category.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(int id);
}
