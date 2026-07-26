using Inventory.Domain.Enums;

namespace Inventory.Application.DTOs.InventoryMovement;

public class InventoryMovementRequest
{
    public MovementType MovementType { get; set; }

}
