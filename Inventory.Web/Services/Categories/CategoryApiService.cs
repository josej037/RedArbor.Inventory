using Inventory.Web.Models.Categories;
using Inventory.Web.Services.http;

namespace Inventory.Web.Services.Categories;
public class CategoryApiService : ICategoryApiService
{
    private readonly IApiClient _client;
    public CategoryApiService(IApiClient client)
    {
        _client = client;
    }
    public async Task<ApiResponse<CategoryDto?>> Create(CategoryDto request)
    {
        var response = await _client.PostAsync<CategoryDto, CategoryDto>("Category", request);
        return ApiResponse<CategoryDto?>.Ok(response!.Data, new ApiMessage("Category", "Category created successfully."));
    }
    public async Task<bool> Delete(int id)
    {
        await _client.DeleteAsync($"Category/{id}");
        return true;
    }
    public async Task<ApiResponse<List<CategoryDto?>?>> GetAll()
    {
        try
        {
            var response = await _client.GetAsync<List<CategoryDto?>?>("Category");
            return ApiResponse<List<CategoryDto?>?>.Ok(response!, new ApiMessage("Category", "Categories retrieved successfully."));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during login: {ex.Message}");
            return ApiResponse<List<CategoryDto?>?>.Fail(new ApiMessage("Category", "List category empty."),
                new List<ApiError> { new ApiError("Category", ex.Message) });
        }
    }
    public async Task<ApiResponse<CategoryDto?>> GetById(int id)
    {
        try
        {
            var response = await _client.GetAsync<CategoryDto?>($"Category/{id}");
            return ApiResponse<CategoryDto?>.Ok(response!, new ApiMessage("Category", "Category retrieved successfully."));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during login: {ex.Message}");
            return ApiResponse<CategoryDto?>.Fail(new ApiMessage("Category", "List category empty."),
                new List<ApiError> { new ApiError("Category", ex.Message) });
        }
    }
    public async Task<bool> Update(int id, CategoryDto request)
    {
        try
        {
            var response = await _client.PutAsync<CategoryDto, CategoryDto>($"Category/{id}", request);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during login: {ex.Message}");
            return false;
        }
    }
}
