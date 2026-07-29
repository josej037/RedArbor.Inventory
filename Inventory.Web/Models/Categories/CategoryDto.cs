namespace Inventory.Web.Models.Categories;

public sealed record CategoryDto(
    int Id,
     string Name,
     string Description,
     bool Active
);
