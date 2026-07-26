using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IInventoryExitRepository
{
    /// <summary>
    /// List all exits.
    /// </summary>
    /// <returns>IEnumerable<InventoryExit></returns>
    Task<IEnumerable<InventoryExit>> GetAll();

    /// <summary>
    /// Gets an inventory exit by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>InventoryExit</returns>
    Task<InventoryExit?> GetById(int id);

    /// <summary>
    /// Creates a new exit.
    /// </summary>
    /// <param name="inventoryExit"></param>
    /// <returns>ID</returns>
    Task<int> Create(InventoryExit inventoryExit);

    /// <summary>
    /// Update an existing exit.
    /// </summary>
    /// <param name="inventoryExit"></param>
    /// <returns></returns>
    Task Update(InventoryExit inventoryExit);

    /// <summary>
    /// Delete a exit.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(int id);
}
