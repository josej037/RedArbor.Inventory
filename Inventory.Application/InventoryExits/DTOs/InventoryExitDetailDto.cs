namespace Inventory.Application.InventoryExits.DTOs;

public sealed record InventoryExitDetailDto(
        int Id,
        int ProductId,
        decimal Quantity,
        decimal UnitCost
);
