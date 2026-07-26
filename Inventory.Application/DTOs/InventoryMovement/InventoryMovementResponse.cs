using Inventory.Application.DTOs.Product;

namespace Inventory.Application.DTOs.InventoryMovement;

public class InventoryMovementResponse
{
    public int ProductId { get; set; }
    public int MovementType { get; set; }
    public int ReferenceId { get; set; }
    public decimal Quantity { get; set; }
    public decimal StockBefore { get; set; }
    public decimal StockAfter { get; set; }

    public ProductResponse Product { get; set; } = new();
}
