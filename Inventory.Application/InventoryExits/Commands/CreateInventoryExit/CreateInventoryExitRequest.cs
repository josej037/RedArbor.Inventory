namespace Inventory.Application.InventoryExits.Commands.CreateInventoryExit;

public class CreateInventoryExitRequest
{
    public string Client { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime DeliveredDate { get; set; }
    public List<CreateInventoryExitDetailRequest> Details { get; set; } = new();
}

