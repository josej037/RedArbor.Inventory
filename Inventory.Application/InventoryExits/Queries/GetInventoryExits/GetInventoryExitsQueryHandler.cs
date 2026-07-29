using Inventory.Application.Interfaces;
using Inventory.Application.InventoryExits.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryExits.Queries.GetInventoryExits;

public class GetInventoryExitsQueryHandler : IRequestHandler<GetInventoryExitsQuery, Result<List<InventoryExitDto>>>
{
    private readonly IInventoryExitRepository _repository;

    public GetInventoryExitsQueryHandler(IInventoryExitRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<InventoryExitDto>>> Handle(GetInventoryExitsQuery request, CancellationToken cancellationToken)
    {
        var exits = await _repository.GetAll();
        var list = exits
         .Select(e => new InventoryExitDto(
             e.Id,
             e.Client,
             e.OrderNumber,
             e.DeliveredDate,
             e.InventoryExitDetails
                 .Select(d => new InventoryExitDetailDto(
                     d.Id,
                     d.ProductId,
                     d.Quantity,
                     d.UnitCost))
                 .ToList()
         ))
         .ToList();

        return Result<List<InventoryExitDto>>.Success(list);
    }

}
