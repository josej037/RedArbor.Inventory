using Inventory.Web.Models.Categories;
using Inventory.Web.Services.http;

namespace Inventory.Web.Services.Categories
{
    public interface ICategoryApiService
    {
        Task<ApiResponse<List<CategoryDto?>?>> GetAll();
        Task<ApiResponse<CategoryDto?>> GetById(int id);
        Task<ApiResponse<CategoryDto?>> Create(CategoryDto request);
        Task<bool> Update(int id, CategoryDto request);
        Task<bool> Delete(int id);
    }
}
