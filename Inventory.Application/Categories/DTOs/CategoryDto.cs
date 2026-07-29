namespace Inventory.Application.Categories.DTOs;

public sealed record CategoryDto(
     int Id,
     string Name,
     string Description,
     bool Active
);
