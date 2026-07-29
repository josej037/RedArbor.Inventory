using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryExits.Commands.DeleteInventoryExit;

public class DeleteInventoryExitCommandHandler : IRequestHandler<DeleteInventoryExitCommand, Result<bool>>
{
    private readonly IInventoryExitRepository _repository;
    public DeleteInventoryExitCommandHandler(IInventoryExitRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<bool>> Handle(DeleteInventoryExitCommand request, CancellationToken cancellationToken)
    {
        var inventoryExit = await _repository.GetById(request.Id);
        if (inventoryExit is null)
        {
            return Result<bool>.Failure(
                new InventoryError(
                    "InventoryExit.NotFound",
                    "The specified inventory exit does not exist."));
        }
        await _repository.Delete(request.Id);
        return Result<bool>.Success(true);
    }
}
