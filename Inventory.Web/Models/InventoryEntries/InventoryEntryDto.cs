namespace Inventory.Web.Models.InventoryEntries;

public sealed record InventoryEntryDto(
     int? Id,
     string Supplier,
     string InvoiceNumber,
     DateTime ReceivedDate,
     List<InventoryEntryDetailDto>? Details
);
