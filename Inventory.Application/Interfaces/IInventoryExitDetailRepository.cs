using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IInventoryExitDetailRepository
{
    /// <summary>
    /// List all exit details.
    /// </summary>
    /// <param name="inventoryExitId"></param>
    /// <returns>IEnumerable<InventoryExitDetail></returns>
    Task<IEnumerable<InventoryExitDetail>> GetByExitId(int inventoryExitId);

    /// <summary>
    /// Creates a new exit detail.
    /// </summary>
    /// <param name="detail"></param>
    /// <returns>ID</returns> 
    Task<int> Create(InventoryExitDetail detail);

    /// <summary>
    /// Deletes a exit detail.
    /// </summary>
    /// <param name="inventoryExitId"></param>
    /// <returns></returns>
    Task DeleteByExitId(int inventoryExitId);
}
