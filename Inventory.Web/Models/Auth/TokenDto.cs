namespace Inventory.Web.Models.Auth;

public sealed record TokenDto(string token, DateTime expiration);
