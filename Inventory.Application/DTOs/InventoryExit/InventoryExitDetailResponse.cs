namespace Inventory.Application.DTOs.InventoryExit;

public class InventoryExitDetailResponse
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal TotalAmount { get; set; }
}
