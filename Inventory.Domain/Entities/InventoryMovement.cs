using Inventory.Domain.Enums;

namespace Inventory.Domain.Entities;

public class InventoryMovement : Base
{
    public int ProductId { get; set; }
    public MovementType MovementType { get; set; }
    public int ReferenceId { get; set; }
    public decimal Quantity { get; set; }
    public decimal StockBefore { get; set; }
    public decimal StockAfter { get; set; }
    public Product Product { get; set; } = default!;

}
