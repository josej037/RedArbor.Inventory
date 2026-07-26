using Inventory.Domain.Entities;

namespace Inventory.Application.DTOs.InventoryExit;

public class InventoryExitRequest
{
    public string Client { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime? DeliveredDate { get; set; }

    public virtual ICollection<InventoryExitDetailRequest> Details { get; set; } = new List<InventoryExitDetailRequest>();

}
