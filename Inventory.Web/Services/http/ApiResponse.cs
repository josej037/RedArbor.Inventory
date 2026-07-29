namespace Inventory.Web.Services.http;

public sealed class ApiResponse<T>
{
    public bool Success { get; init; }

    public ApiMessage? Message { get; init; }

    public T? Data { get; init; }

    public IEnumerable<ApiError>? Errors { get; init; }

    public static ApiResponse<T> Ok(
        T data,
        ApiMessage message)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static ApiResponse<T> Fail(
        ApiMessage message,
        IEnumerable<ApiError>? errors = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors
        };
    }
}
public sealed record ApiMessage(
    string Code,
    string Message);

public sealed record ApiError(
    string Code,
    string Description);