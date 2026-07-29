namespace Inventory.Application.InventoryEntries.Commands.CreateInventoryEntry;

public class CreateInventoryEntryDetailRequest
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
}
