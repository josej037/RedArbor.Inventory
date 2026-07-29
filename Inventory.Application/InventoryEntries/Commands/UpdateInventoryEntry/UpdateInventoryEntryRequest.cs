
namespace Inventory.Application.InventoryEntries.Commands.UpdateInventoryEntry;

public class UpdateInventoryEntryRequest
{
    public string Supplier { get; set; } = string.Empty;
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime ReceivedDate { get; set; }
    public List<UpdateInventoryEntryDetailRequest> Details { get; set; } = new();
}
