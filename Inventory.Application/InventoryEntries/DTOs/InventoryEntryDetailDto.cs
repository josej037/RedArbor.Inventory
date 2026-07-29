namespace Inventory.Application.InventoryEntries.DTOs;

public sealed record InventoryEntryDetailDto(
        int? Id,
        int ProductId,
        decimal Quantity,
        decimal UnitCost
);
