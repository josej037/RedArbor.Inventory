using Inventory.Application.Interfaces;
using Inventory.Application.InventoryExits.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryExits.Queries.GetInventoryExitById;

public class GetInventoryExitByIdQueryHandler : IRequestHandler<GetInventoryExitByIdQuery, Result<InventoryExitDto?>>
{
    private readonly IInventoryExitRepository _repository;
    public GetInventoryExitByIdQueryHandler(IInventoryExitRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<InventoryExitDto?>> Handle(GetInventoryExitByIdQuery request, CancellationToken cancellationToken)
    {
        var inventoryExit = await _repository.GetById(request.Id);
        if (inventoryExit is null)
        {
            return Result<InventoryExitDto?>.Failure(new InventoryError(
                    "InventoryExit.NotFound",
                    "Inventory exit not found"));
        }
        return Result<InventoryExitDto?>.Success(new InventoryExitDto(
            inventoryExit.Id,
            inventoryExit.Client,
            inventoryExit.OrderNumber,
            inventoryExit.DeliveredDate,
            inventoryExit.InventoryExitDetails
                .Select(d => new InventoryExitDetailDto(
                    d.Id,
                    d.ProductId,
                    d.Quantity,
                    d.UnitCost))
                .ToList()
        ));
    }
}
