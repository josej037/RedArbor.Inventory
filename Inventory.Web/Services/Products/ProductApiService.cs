using Inventory.Web.Models.Products;
using Inventory.Web.Services.http;

namespace Inventory.Web.Services.Products
{
    public class ProductApiService : IProductApiService
    {
        private readonly IApiClient _client;

        public ProductApiService(IApiClient client)
        {
            _client = client;
        }

        public async Task<ProductDto?> Create(ProductDto request)
        {
            //return await _client.PostAsync<ProductDto, ProductDto>("Product", request);
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            await _client.DeleteAsync($"Product/{id}");
            return true;
        }

        public async Task<List<ProductDto?>?> GetAll()
        {
            //return await _client.GetAsync<List<ProductDto?>?>("Product");
            return null;
        }

        public async Task<ProductDto?> GetById(int id)
        {
            //return await _client.GetAsync<ProductDto?>($"Product/{id}");
            return null;
        }

        public async Task<bool> Update(int id, ProductDto request)
        {
            await _client.PutAsync<ProductDto, ProductDto>($"Product/{id}", request);
            return true;
        }
    }
}
