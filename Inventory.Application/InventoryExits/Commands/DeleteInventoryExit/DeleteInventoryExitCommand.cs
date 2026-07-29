using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryExits.Commands.DeleteInventoryExit;

public sealed record DeleteInventoryExitCommand(int Id) : IRequest<Result<bool>>;
