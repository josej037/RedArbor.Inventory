using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryEntries.Commands.DeleteInventoryEntry;

public class DeleteInventoryEntryCommandHandler : IRequestHandler<DeleteInventoryEntryCommand, Result<bool>>
{
    private readonly IInventoryEntryRepository _repository;
    public DeleteInventoryEntryCommandHandler(IInventoryEntryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<bool>> Handle(DeleteInventoryEntryCommand request, CancellationToken cancellationToken)
    {
        var inventoryEntry = await _repository.GetById(request.Id);
        if (inventoryEntry is null)
        {
            return Result<bool>.Failure(
                new InventoryError(
                    "InventoryEntry.NotFound",
                    "The specified inventory entry does not exist."));
        }

        await _repository.Delete(request.Id);
        return Result<bool>.Success(true);
    }
}
