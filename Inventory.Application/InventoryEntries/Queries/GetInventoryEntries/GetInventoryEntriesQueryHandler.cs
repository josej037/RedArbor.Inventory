using Inventory.Application.Interfaces;
using Inventory.Application.InventoryEntries.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryEntries.Queries.GetInventoryEntries;

public class GetInventoryEntriesQueryHandler : IRequestHandler<GetInventoryEntriesQuery, Result<List<InventoryEntryDto>>>
{
    private readonly IInventoryEntryRepository _repository;

    public GetInventoryEntriesQueryHandler(IInventoryEntryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<InventoryEntryDto>>> Handle(GetInventoryEntriesQuery request, CancellationToken cancellationToken)
    {
        var entries = await _repository.GetAll();
        var list = entries
         .Select(e => new InventoryEntryDto(
             e.Id,
             e.Supplier,
             e.InvoiceNumber,
             e.ReceivedDate,
             e.InventoryEntryDetails
                 .Select(d => new InventoryEntryDetailDto(
                     d.Id,
                     d.ProductId,
                     d.Quantity,
                     d.UnitCost))
                 .ToList()
         ))
         .ToList();

        return Result<List<InventoryEntryDto>>.Success(list);
    }

}
