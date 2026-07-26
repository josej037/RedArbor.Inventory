using Inventory.Application.DTOs.InventoryEntry;

namespace Inventory.Application.Services.Interfaces;

public interface IInventoryEntryService
{
    Task<IEnumerable<InventoryEntryResponse>> GetAll();
    Task<InventoryEntryResponse?> GetById(int id);
    Task<InventoryEntryResponse> Create(InventoryEntryRequest request);
    Task Update(int id, InventoryEntryRequest request);
    Task Delete(int id);
}
