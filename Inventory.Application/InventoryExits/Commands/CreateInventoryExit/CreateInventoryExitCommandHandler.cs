using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryExits.Commands.CreateInventoryExit;

public sealed class CreateInventoryExitCommandHandler : IRequestHandler<CreateInventoryExitCommand, Result<int>>
{
    private readonly IInventoryExitRepository _repository;
    public CreateInventoryExitCommandHandler(IInventoryExitRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> Handle(CreateInventoryExitCommand request, CancellationToken cancellationToken)
    {
        var inventoryExit = new Domain.Entities.InventoryExit
        {
            Client = request.InventoryExit.Client,
            OrderNumber = request.InventoryExit.OrderNumber,
            DeliveredDate = request.InventoryExit.DeliveredDate,
            InventoryExitDetails = request.InventoryExit.Details.Select(d => new Domain.Entities.InventoryExitDetail
            {
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitCost = d.UnitCost
            }).ToList()
        };
        var result = await _repository.Create(inventoryExit);
        return Result<int>.Success(result);
    }
}

