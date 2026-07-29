using Inventory.Web.Models.Auth;
using Inventory.Web.Services.http;

namespace Inventory.Web.Services.Auth
{
    public interface IAuthApiService
    {
        Task<ApiResponse<TokenDto?>> Login(LoginDto request);
    }
}
