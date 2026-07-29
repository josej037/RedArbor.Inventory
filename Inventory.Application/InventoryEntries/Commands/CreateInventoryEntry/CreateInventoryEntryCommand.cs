using Inventory.Application.InventoryEntries.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryEntries.Commands.CreateInventoryEntry;

public sealed record CreateInventoryEntryCommand(InventoryEntryDto InventoryEntry) : IRequest<Result<int>>;
