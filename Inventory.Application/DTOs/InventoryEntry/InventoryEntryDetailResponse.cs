
namespace Inventory.Application.DTOs.InventoryEntry;

public class InventoryEntryDetailResponse
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal TotalAmount { get; set; }
}
