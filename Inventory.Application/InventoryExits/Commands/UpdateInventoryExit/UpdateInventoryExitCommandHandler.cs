using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryExits.Commands.UpdateInventoryExit;

public sealed class UpdateInventoryExitCommandHandler : IRequestHandler<UpdateInventoryExitCommand, Result<bool>>
{
    private readonly IInventoryExitRepository _repository;

    private readonly IInventoryExitDetailRepository _repositoryDetail;

    public UpdateInventoryExitCommandHandler(IInventoryExitRepository repository, IInventoryExitDetailRepository repositoryDetail)
    {
        _repository = repository;
        _repositoryDetail = repositoryDetail;
    }

    public async Task<Result<bool>> Handle(UpdateInventoryExitCommand request, CancellationToken cancellationToken)
    {
        var inventoryExit = await _repository.GetById(request.Id);
        if (inventoryExit is null)
        {
            return Result<bool>.Failure(
                new InventoryError(
                    "InventoryExit.NotFound",
                    "The specified inventory exit does not exist."));
        }
        inventoryExit.Client = request.Request.Client;
        inventoryExit.OrderNumber = request.Request.OrderNumber;
        inventoryExit.DeliveredDate = request.Request.DeliveredDate;
        inventoryExit.UpdatedAt = DateTime.UtcNow;
        await _repository.Update(inventoryExit);

        return Result<bool>.Success(true);
    }
}
