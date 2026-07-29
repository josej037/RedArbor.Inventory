namespace Inventory.Application.InventoryExits.Commands.UpdateInventoryExit;

public class UpdateInventoryExitDetailRequest
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
}
