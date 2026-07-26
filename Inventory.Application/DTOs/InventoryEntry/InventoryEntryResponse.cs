
namespace Inventory.Application.DTOs.InventoryEntry;

public class InventoryEntryResponse
{
    public int Id { get; set; }

    public string Supplier { get; set; } = string.Empty;

    public string InvoiceNumber { get; set; } = string.Empty;

    public DateTime ReceivedDate { get; set; }

    public List<InventoryEntryDetailResponse> Details { get; set; } = new();
}
