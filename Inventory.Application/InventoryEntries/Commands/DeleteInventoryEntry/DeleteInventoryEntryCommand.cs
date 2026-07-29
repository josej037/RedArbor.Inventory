using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryEntries.Commands.DeleteInventoryEntry;
public sealed record DeleteInventoryEntryCommand(int Id) : IRequest<Result<bool>>;
