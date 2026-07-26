namespace Inventory.Application.DTOs.InventoryExit;

public class InventoryExitResponse
{
    public int Id { get; set; }
    public string Client { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime? DeliveredDate { get; set; }
    public List<InventoryExitDetailResponse> Details { get; set; } = new();
}
