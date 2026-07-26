using Inventory.Application.DTOs.InventoryMovement;
using Inventory.Domain.Enums;

namespace Inventory.Application.Services.Interfaces;

public interface IInventoryMovementService
{
    Task<IEnumerable<InventoryMovementResponse>> GetAllByMovementType(int MovementType);
}
