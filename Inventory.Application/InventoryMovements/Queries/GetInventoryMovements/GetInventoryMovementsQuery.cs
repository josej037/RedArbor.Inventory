using Inventory.Application.InventoryMovements.DTOs;
using Inventory.Application.Results;
using Inventory.Domain.Enums;
using MediatR;

namespace Inventory.Application.InventoryMovements.Queries.GetInventoryMovements;

public sealed record GetInventoryMovementQuery(MovementType MovementType) : IRequest<Result<List<InventoryMovementDto?>>>;
