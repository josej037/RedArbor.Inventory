using Inventory.Web.Models.InventoryEntries;
using Inventory.Web.Services.http;

namespace Inventory.Web.Services.InventoryEntries
{
    public class InventoryEntryApiService : IInventoryEntryApiService
    {
        private readonly IApiClient _client;

        public InventoryEntryApiService(IApiClient client)
        {
            _client = client;
        }

        public async Task<InventoryEntryDto?> Create(InventoryEntryDto request)
        {
            //return await _client.PostAsync<InventoryEntryDto, InventoryEntryDto>("InventoryEntry", request);
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            await _client.DeleteAsync($"InventoryEntry/{id}");
            return true;
        }

        public async Task<List<InventoryEntryDto?>?> GetAll()
        {
            //return await _client.GetAsync<List<InventoryEntryDto?>?>("InventoryEntry");
            return null;
        }

        public async Task<InventoryEntryDto?> GetById(int id)
        {
            //return await _client.GetAsync<InventoryEntryDto?>($"InventoryEntry/{id}");
            return null;
        }

        public async Task<bool> Update(int id, InventoryEntryDto request)
        {
            await _client.PutAsync<InventoryEntryDto, InventoryEntryDto>($"InventoryEntry/{id}", request);
            return true;
        }
    }
}
