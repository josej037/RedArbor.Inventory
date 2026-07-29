namespace Inventory.Application.InventoryMovements.DTOs;
public sealed record InventoryMovementDto(
int ProductId,
  int MovementType,
  int ReferenceId,
  decimal Quantity,
  decimal StockBefore,
  decimal StockAfter
);