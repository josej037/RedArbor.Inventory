using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryEntries.Commands.CreateInventoryEntry;

public sealed class CreateInventoryEntryCommandHandler : IRequestHandler<CreateInventoryEntryCommand, Result<int>>
{
    private readonly IInventoryEntryRepository _repository;
    private readonly IInventoryEntryDetailRepository _repositoryDetail;

    public CreateInventoryEntryCommandHandler(IInventoryEntryRepository repository, IInventoryEntryDetailRepository repositoryDetail)
    {
        _repository = repository;
        _repositoryDetail = repositoryDetail;
    }

    public async Task<Result<int>> Handle(CreateInventoryEntryCommand request, CancellationToken cancellationToken)
    {
        var inventoryEntry = new Domain.Entities.InventoryEntry
        {
            Supplier = request.InventoryEntry.Supplier,
            InvoiceNumber = request.InventoryEntry.InvoiceNumber,
            ReceivedDate = request.InventoryEntry.ReceivedDate,
            InventoryEntryDetails = request.InventoryEntry.Details.Select(d => new Domain.Entities.InventoryEntryDetail
            {
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitCost = d.UnitCost
            }).ToList()
        };
        var result = await _repository.Create(inventoryEntry);


        await _repositoryDetail.Create(
            request.InventoryEntry.Details.Select(d => new Domain.Entities.InventoryEntryDetail
            {
                InventoryEntryId = result,
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitCost = d.UnitCost
            }));



        return Result<int>.Success(result);
    }
}

