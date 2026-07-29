using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryEntries.Commands.UpdateInventoryEntry;

public sealed class UpdateInventoryEntryCommandHandler : IRequestHandler<UpdateInventoryEntryCommand, Result<bool>>
{
    private readonly IInventoryEntryRepository _repository;
    private readonly IInventoryEntryDetailRepository _repositoryDetail;

    public UpdateInventoryEntryCommandHandler(IInventoryEntryRepository repository, IInventoryEntryDetailRepository repositoryDetail)
    {
        _repository = repository;
        _repositoryDetail = repositoryDetail;
    }

    public async Task<Result<bool>> Handle(UpdateInventoryEntryCommand request, CancellationToken cancellationToken)
    {
        var inventoryEntry = await _repository.GetById(request.Id);
        if (inventoryEntry is null)
        {
            return Result<bool>.Failure(
                new InventoryError(
                    "InventoryEntry.NotFound",
                    "The specified inventory entry does not exist."));
        }
        inventoryEntry.Supplier = request.Request.Supplier;
        inventoryEntry.InvoiceNumber = request.Request.InvoiceNumber;
        inventoryEntry.ReceivedDate = request.Request.ReceivedDate;
        inventoryEntry.UpdatedAt = DateTime.UtcNow;
        await _repository.Update(inventoryEntry);
        await _repositoryDetail.Create(
            request.Request.Details!.Select(d => new Domain.Entities.InventoryEntryDetail
            {
                InventoryEntryId = inventoryEntry.Id,
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitCost = d.UnitCost
            }));

        return Result<bool>.Success(true);
    }
}
