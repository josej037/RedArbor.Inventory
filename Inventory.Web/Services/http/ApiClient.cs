using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Inventory.Web.Services.http;

public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContext;
    public ApiClient(HttpClient httpClient, IHttpContextAccessor httpContext)
    {
        _httpClient = httpClient;
        _httpContext = httpContext;
    }

    private async Task AddAuthorizationHeader()
    {
        var token = await _httpContext.HttpContext!.GetTokenAsync("access_token");
        _httpClient.DefaultRequestHeaders.Authorization = null;
        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }
    private static async Task EnsureSuccess(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;
        var error = await response.Content.ReadAsStringAsync();
        throw new ApiException(response.StatusCode, error);
    }
    public async Task<T?> GetAsync<T>(string endpoint)
    {
        await AddAuthorizationHeader();

        var response = await _httpClient.GetAsync(endpoint);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        if (json is null)
            throw new Exception("Empty response.");

        using var document = JsonDocument.Parse(json);
        var value = document.RootElement.GetProperty("value");

        var result = new ApiResponse<T>
        {
            Success = true,
            Data = value.Deserialize<T>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })
        };

        if (!result.Success)
            throw new ApiException(response.StatusCode, "Empty response.");

        return result.Data;
    }


    public async Task<ApiResponse<TResponse>?> PostAsync<TRequest, TResponse>(string endpoint, TRequest request)
    {
        await AddAuthorizationHeader();
        var response = await _httpClient.PostAsJsonAsync(endpoint, request);
        await EnsureSuccess(response);
        var json = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(json);
        var value = document.RootElement.GetProperty("value");


        var result = new ApiResponse<TResponse>
        {
            Success = true,
            Data = value.Deserialize<TResponse>()
        };
        return result;
        //return await response.Content.ReadFromJsonAsync<ApiResponse<TResponse>>();
    }

    public async Task<ApiResponse<TResponse>?> PutAsync<TRequest, TResponse>(string endpoint, TRequest request)
    {
        await AddAuthorizationHeader();
        var response = await _httpClient.PutAsJsonAsync(endpoint, request);
        await EnsureSuccess(response);
        return await response.Content.ReadFromJsonAsync<ApiResponse<TResponse>>();
    }
    public async Task DeleteAsync(string endpoint)
    {
        await AddAuthorizationHeader();
        var response = await _httpClient.DeleteAsync(endpoint);
        await EnsureSuccess(response);
    }
}