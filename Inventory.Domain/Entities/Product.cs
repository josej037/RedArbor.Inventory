namespace Inventory.Domain.Entities;

public class Product : Base
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Stock { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public virtual ICollection<InventoryEntryDetail> InventoryEntryDetails { get; set; } = new List<InventoryEntryDetail>();
    public virtual ICollection<InventoryExitDetail> InventoryExitDetails { get; set; } = new List<InventoryExitDetail>();
    public virtual ICollection<InventoryMovement> InventoryMovements { get; set; } = new List<InventoryMovement>();
}
