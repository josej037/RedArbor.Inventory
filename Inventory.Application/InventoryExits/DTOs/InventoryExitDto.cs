namespace Inventory.Application.InventoryExits.DTOs;

public sealed record InventoryExitDto(
    int Id,
    string Client,
    string OrderNumber,
    DateTime? DeliveredDate,
    ICollection<InventoryExitDetailDto> Details
);