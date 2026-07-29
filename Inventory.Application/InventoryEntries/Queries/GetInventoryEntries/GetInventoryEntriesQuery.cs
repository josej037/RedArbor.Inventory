using Inventory.Application.InventoryEntries.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryEntries.Queries.GetInventoryEntries;

public sealed record GetInventoryEntriesQuery : IRequest<Result<List<InventoryEntryDto>>>;
