using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IInventoryEntryDetailRepository
{
    Task<IEnumerable<InventoryEntryDetail>> GetByEntryId(int inventoryEntryId);
    Task<int> Create(InventoryEntryDetail detail);
    Task DeleteByEntryId(int inventoryEntryId);
}
