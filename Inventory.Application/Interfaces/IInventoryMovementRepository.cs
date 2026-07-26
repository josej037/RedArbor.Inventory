using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IInventoryMovementRepository
{
    Task<int> Create(InventoryMovement movement);
}
