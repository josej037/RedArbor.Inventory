using Inventory.Application.Interfaces;
using Inventory.Application.InventoryEntries.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryEntries.Queries.GetInventoryEntryById;

public class GetInventoryEntryByIdQueryHandler : IRequestHandler<GetInventoryEntryByIdQuery, Result<InventoryEntryDto?>>
{
    private readonly IInventoryEntryRepository _repository;
    public GetInventoryEntryByIdQueryHandler(IInventoryEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<InventoryEntryDto?>> Handle(GetInventoryEntryByIdQuery request, CancellationToken cancellationToken)
    {
        var inventoryEntry = await _repository.GetById(request.Id);
        if (inventoryEntry is null)
        {
            return Result<InventoryEntryDto?>.Failure(new InventoryError(
                    "InventoryEntry.NotFound",
                    "Inventory entry not found"));
        }
        return Result<InventoryEntryDto?>.Success(new InventoryEntryDto(
            inventoryEntry.Id,
            inventoryEntry.Supplier,
            inventoryEntry.InvoiceNumber,
            inventoryEntry.ReceivedDate,
            inventoryEntry.InventoryEntryDetails
                .Select(d => new InventoryEntryDetailDto(
                    d.Id,
                    d.ProductId,
                    d.Quantity,
                    d.UnitCost))
                .ToList()
        ));
    }
}
