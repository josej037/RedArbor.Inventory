using Inventory.Application.InventoryEntries.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryEntries.Commands.UpdateInventoryEntry;

public sealed record UpdateInventoryEntryCommand(int Id, InventoryEntryDto Request) : IRequest<Result<bool>>;


