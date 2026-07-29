namespace Inventory.Web.Services.http
{
    public interface IApiClient
    {
        Task<T?> GetAsync<T>(string endpoint);
        Task<ApiResponse<TResponse>?> PostAsync<TRequest, TResponse>(string endpoint, TRequest request);
        Task<ApiResponse<TResponse>?> PutAsync<TRequest, TResponse>(string endpoint, TRequest request);
        Task DeleteAsync(string endpoint);
    }
}
