namespace Inventory.Application.InventoryEntries.Commands.UpdateInventoryEntry;

public class UpdateInventoryEntryDetailRequest
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
}
