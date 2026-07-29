using Inventory.Application.InventoryExits.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryExits.Commands.CreateInventoryExit;

public sealed record CreateInventoryExitCommand(InventoryExitDto InventoryExit) : IRequest<Result<int>>;
