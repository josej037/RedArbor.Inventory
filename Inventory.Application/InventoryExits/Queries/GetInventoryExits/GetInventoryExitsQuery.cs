using Inventory.Application.InventoryExits.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryExits.Queries.GetInventoryExits;

public sealed record GetInventoryExitsQuery : IRequest<Result<List<InventoryExitDto>>>;
