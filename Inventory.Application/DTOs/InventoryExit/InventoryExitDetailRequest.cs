namespace Inventory.Application.DTOs.InventoryExit;

public class InventoryExitDetailRequest
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
}
