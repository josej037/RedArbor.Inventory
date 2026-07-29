using Inventory.Application.InventoryExits.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryExits.Queries.GetInventoryExitById;

public sealed record GetInventoryExitByIdQuery(int Id) : IRequest<Result<InventoryExitDto?>>;
