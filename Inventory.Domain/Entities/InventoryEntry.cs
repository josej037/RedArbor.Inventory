namespace Inventory.Domain.Entities;

public class InventoryEntry : Base
{
    public string Supplier { get; set; } = string.Empty;
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime ReceivedDate { get; set; }
    public virtual ICollection<InventoryEntryDetail> InventoryEntryDetails { get; set; } = new List<InventoryEntryDetail>();
}
