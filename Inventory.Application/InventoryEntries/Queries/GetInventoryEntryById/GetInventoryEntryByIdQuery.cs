using Inventory.Application.InventoryEntries.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryEntries.Queries.GetInventoryEntryById;

public sealed record GetInventoryEntryByIdQuery(int Id) : IRequest<Result<InventoryEntryDto?>>;
