namespace Inventory.Application.Auth.DTOs;

public sealed record TokenDto(string token, DateTime expiration);
