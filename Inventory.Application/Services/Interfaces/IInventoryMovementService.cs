using Inventory.Application.DTOs.InventoryMovement;

namespace Inventory.Application.Services.Interfaces;

public interface IInventoryMovementService
{
    Task<IEnumerable<InventoryMovementResponse>> GetAllByMovementType(int MovementType);
}
