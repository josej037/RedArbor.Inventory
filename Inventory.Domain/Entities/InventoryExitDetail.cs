namespace Inventory.Domain.Entities;

public class InventoryExitDetail : Base
{
    public int InventoryExitId { get; set; }
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal TotalAmount { get; set; }

    public InventoryExit InventoryExit { get; set; } = default!;
    public Product Product { get; set; } = default!;
}
