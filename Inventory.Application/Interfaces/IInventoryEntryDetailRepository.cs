using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IInventoryEntryDetailRepository
{
    /// <summary>
    /// List all entry details.
    /// </summary>
    /// <param name="inventoryEntryId"></param>
    /// <returns>IEnumerable<InventoryEntryDetail></returns>
    Task<IEnumerable<InventoryEntryDetail>> GetByEntryId(int inventoryEntryId);

    /// <summary>
    /// Creates a new entry detail.
    /// </summary>
    /// <param name="detail"></param>
    /// <returns>ID</returns> 
    Task<int> Create(IEnumerable<InventoryEntryDetail> details);

    /// <summary>
    /// Deletes a entry detail.
    /// </summary>
    /// <param name="inventoryEntryId"></param>
    /// <returns></returns>
    Task DeleteByEntryId(int inventoryEntryId);
}
