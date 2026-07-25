namespace Inventory.Domain.Entities
{
    public abstract class Base
    {
        public int Id { get; set; }
        public bool active { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
