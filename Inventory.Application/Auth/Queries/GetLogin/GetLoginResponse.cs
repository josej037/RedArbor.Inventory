namespace Inventory.Application.Auth.Queries.GetLogin;

public sealed record GetLoginResponse(string token, DateTime expiration);
