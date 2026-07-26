using Inventory.Application.DTOs.InventoryExit;

namespace Inventory.Application.Services.Interfaces;

public interface IInventoryExitService
{
    Task<IEnumerable<InventoryExitResponse>> GetAll();
    Task<InventoryExitResponse?> GetById(int id);
    Task<InventoryExitResponse> Create(InventoryExitRequest request);
    Task Update(int id, InventoryExitRequest request);
    Task Delete(int id);
}
