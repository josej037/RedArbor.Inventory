namespace Inventory.Application.InventoryEntries.DTOs;

public sealed record InventoryEntryDto(
     int? Id,
        string Supplier,
        string InvoiceNumber,
        DateTime ReceivedDate,
        List<InventoryEntryDetailDto>? Details
);
