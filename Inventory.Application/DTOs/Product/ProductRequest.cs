namespace Inventory.Application.DTOs.Product;

public class ProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Stock { get; set; }
    public int CategoryId { get; set; }
}
