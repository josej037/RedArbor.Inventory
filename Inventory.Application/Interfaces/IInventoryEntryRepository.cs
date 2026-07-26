using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IInventoryEntryRepository
{
    Task<IEnumerable<InventoryEntry>> GetAll();
    Task<InventoryEntry?> GetById(int id);
    Task<int> Create(InventoryEntry inventoryEntry);
    Task Update(InventoryEntry inventoryEntry);
    Task Delete(int id);
}
