namespace Inventory.Web.Models.InventoryExits;

public sealed record InventoryExitDetailDto(
        int Id,
        int ProductId,
        decimal Quantity,
        decimal UnitCost
);
