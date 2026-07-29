namespace Inventory.Application.InventoryEntries.Commands.CreateInventoryEntry;

public class CreateInventoryEntryRequest
{
    public string Supplier { get; set; } = string.Empty;
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime ReceivedDate { get; set; }
    public List<CreateInventoryEntryDetailRequest> Details { get; set; } = new();
}

