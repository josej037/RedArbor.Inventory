using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IInventoryExitDetailRepository
{
    Task<IEnumerable<InventoryExitDetail>> GetByExitId(int inventoryExitId);
    Task<int> Create(InventoryExitDetail detail);
    Task DeleteByExitId(int inventoryExitId);
}
