namespace Inventory.Domain.Entities;

public class InventoryExit : Base
{
    public string Client { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime? DeliveredDate { get; set; }

    public virtual ICollection<InventoryExitDetail> InventoryExitDetails { get; set; } = new List<InventoryExitDetail>();
}
