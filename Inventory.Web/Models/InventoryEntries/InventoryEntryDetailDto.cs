namespace Inventory.Web.Models.InventoryEntries;

public sealed record InventoryEntryDetailDto(
        int? Id,
        int ProductId,
        decimal Quantity,
        decimal UnitCost
);
