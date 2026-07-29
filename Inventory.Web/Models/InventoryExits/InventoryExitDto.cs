namespace Inventory.Web.Models.InventoryExits;

public sealed record InventoryExitDto(
    int Id,
    string Client,
    string OrderNumber,
    DateTime? DeliveredDate,
    ICollection<InventoryExitDetailDto> Details
);