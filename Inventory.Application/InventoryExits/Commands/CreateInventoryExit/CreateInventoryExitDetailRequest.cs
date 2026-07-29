namespace Inventory.Application.InventoryExits.Commands.CreateInventoryExit;

public class CreateInventoryExitDetailRequest
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
}
