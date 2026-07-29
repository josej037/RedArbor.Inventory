using Inventory.Application.InventoryExits.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryExits.Commands.UpdateInventoryExit;

public sealed record UpdateInventoryExitCommand(int Id, InventoryExitDto Request) : IRequest<Result<bool>>;


