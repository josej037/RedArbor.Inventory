namespace Inventory.Application.Auth.Queries.GetLogin;

public class GetLoginRequest
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
