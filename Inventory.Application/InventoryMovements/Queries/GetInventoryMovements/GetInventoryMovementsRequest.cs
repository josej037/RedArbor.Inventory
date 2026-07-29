using Inventory.Domain.Enums;

namespace Inventory.Application.InventoryMovements.Queries.GetInventoryMovements;

public sealed record GetInventoryMovementsRequest
(
    MovementType movementType
);
