using Inventory.Web.Models.Categories;
using Inventory.Web.Models.InventoryEntries;

namespace Inventory.Web.Services.InventoryEntries
{
    public interface IInventoryEntryApiService
    {
        Task<List<InventoryEntryDto?>?> GetAll();
        Task<InventoryEntryDto?> GetById(int id);
        Task<InventoryEntryDto?> Create(InventoryEntryDto request);
        Task<bool> Update(int id, InventoryEntryDto request);
        Task<bool> Delete(int id);
    }
}
