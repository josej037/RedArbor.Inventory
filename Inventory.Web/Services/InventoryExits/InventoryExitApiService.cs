using Inventory.Web.Models.InventoryExits;
using Inventory.Web.Services.http;

namespace Inventory.Web.Services.InventoryExits;

public class InventoryExitApiService : IInventoryExitApiService
{
    private readonly IApiClient _client;

    public InventoryExitApiService(IApiClient client)
    {
        _client = client;
    }

    public async Task<InventoryExitDto?> Create(InventoryExitDto request)
    {
        //return await _client.PostAsync<InventoryExitDto, InventoryExitDto>("InventoryExit", request);
        return null;
    }

    public async Task<bool> Delete(int id)
    {
        await _client.DeleteAsync($"InventoryExit/{id}");
        return true;
    }

    public async Task<List<InventoryExitDto?>?> GetAll()
    {
        //return await _client.GetAsync<List<InventoryExitDto?>?>("InventoryExit");
        return null;
    }

    public async Task<InventoryExitDto?> GetById(int id)
    {
        //return await _client.GetAsync<InventoryExitDto?>($"InventoryExit/{id}");
        return null;
    }

    public async Task<bool> Update(int id, InventoryExitDto request)
    {
        await _client.PutAsync<InventoryExitDto, InventoryExitDto>($"InventoryExit/{id}", request);
        return true;
    }
}
