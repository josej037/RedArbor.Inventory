using Inventory.Web.Models.Auth;
using Inventory.Web.Models.Categories;
using Inventory.Web.Services.http;

namespace Inventory.Web.Services.Auth;

public class AuthApiService : IAuthApiService
{
    private readonly IApiClient _client;

    public AuthApiService(IApiClient client)
    {
        _client = client;
    }

    public async Task<ApiResponse<TokenDto?>> Login(LoginDto request)
    {
        try
        {
            var response = await _client.PostAsync<LoginDto, TokenDto>("auth", request);
            return ApiResponse<TokenDto?>.Ok(response!.Data
                , new ApiMessage("LoginSuccess", "Login successful."));
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            Console.WriteLine($"An error occurred during login: {ex.Message}");
            return ApiResponse<TokenDto?>.Fail(new ApiMessage("LoginError", "User or password is incorrect."),
                new List<ApiError> { new ApiError("LoginError", ex.Message) });
        }
    }
}

