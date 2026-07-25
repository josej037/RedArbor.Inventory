namespace Inventory.Domain.Entities;

public class InventoryEntryDetail : Base
{
    public int InventoryEntryId { get; set; }
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal TotalAmount => Quantity * UnitCost;

    public InventoryEntry InventoryEntry { get; set; } = default!;
    public Product Product { get; set; } = default!;
}
