
namespace Inventory.Application.DTOs.InventoryEntry;

public class InventoryEntryDetailRequest
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
}
