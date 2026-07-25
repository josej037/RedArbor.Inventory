namespace Inventory.Domain.Entities;

public class Base
{
    public int Id { get; set; }
    public bool Active { get; set; } = true;
    public int? UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
