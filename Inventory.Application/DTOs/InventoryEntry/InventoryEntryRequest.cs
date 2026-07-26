namespace Inventory.Application.DTOs.InventoryEntry;

public class InventoryEntryRequest
{
    public string Supplier { get; set; } = string.Empty;
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime ReceivedDate { get; set; }
    public List<InventoryEntryDetailRequest> Details { get; set; } = new();
}
