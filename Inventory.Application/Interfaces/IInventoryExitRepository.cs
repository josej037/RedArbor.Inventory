using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IInventoryExitRepository
{
    Task<IEnumerable<InventoryExit>> GetAll();
    Task<InventoryExit?> GetById(int id);
    Task<int> Create(InventoryExit inventoryExit);
    Task Update(InventoryExit inventoryExit);
    Task Delete(int id);
}
