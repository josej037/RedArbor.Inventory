using Inventory.Web.Models.InventoryExits;

namespace Inventory.Web.Services.InventoryExits;

public interface IInventoryExitApiService
{
    Task<List<InventoryExitDto?>?> GetAll();
    Task<InventoryExitDto?> GetById(int id);
    Task<InventoryExitDto?> Create(InventoryExitDto request);
    Task<bool> Update(int id, InventoryExitDto request);
    Task<bool> Delete(int id);
}
