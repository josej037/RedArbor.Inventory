using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IInventoryEntryRepository
{
    /// <summary>
    /// List all entries.
    /// </summary>
    /// <returns>IEnumerable<InventoryEntry></returns>
    Task<IEnumerable<InventoryEntry>> GetAll();

    /// <summary>
    /// Gets an inventory entry by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>InventoryEntry</returns>
    Task<InventoryEntry?> GetById(int id);

    /// <summary>
    /// Creates a new entry.
    /// </summary>
    /// <param name="inventoryEntry"></param>
    /// <returns>ID</returns>
    Task<int> Create(InventoryEntry inventoryEntry);

    /// <summary>
    /// Update an existing entry.
    /// </summary>
    /// <param name="inventoryEntry"></param>
    /// <returns></returns>
    Task Update(InventoryEntry inventoryEntry);

    /// <summary>
    /// Delete a entry.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(int id);
}
