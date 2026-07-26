using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IProductRepository
{
    /// <summary>
    /// List all products.
    /// </summary>
    /// <returns>IEnumerable<Product></returns>
    Task<IEnumerable<Product>> GetAll();

    /// <summary>
    /// Gets a product by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Product</returns>
    Task<Product?> GetById(int id);

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <returns>ID</returns>
    Task<int> Create(Product product);

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task Update(Product product);

    /// <summary>
    /// Deletes a product.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(int id);
}
