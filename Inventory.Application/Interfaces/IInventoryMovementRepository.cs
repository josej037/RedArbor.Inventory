using Inventory.Domain.Entities;
using Inventory.Domain.Enums;

namespace Inventory.Application.Interfaces;

public interface IInventoryMovementRepository
{
    /// <summary>
    /// Creates a new inventory movement.
    /// </summary>
    /// <param name="movement"></param>
    /// <returns>ID</returns>
    Task<int> Create(InventoryMovement movement);

    /// <summary>
    /// List all inventory movements by type of entries/exits.
    /// </summary>
    /// <param name="MovementType"></param>
    /// <returns>IEnumerable<InventoryMovement></returns>
    Task<IEnumerable<InventoryMovement>> GetAllByMovementType(MovementType MovementType);
}
