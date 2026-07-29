namespace Inventory.Web.Models.Products;

public sealed record ProductDto(
    int Id, 
    string Name, 
    string Description, 
    decimal Price, 
    decimal Stock, 
    int CategoryId, 
    bool Active);
