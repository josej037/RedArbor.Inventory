using Inventory.Application.Interfaces;
using Inventory.Application.InventoryMovements.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.InventoryMovements.Queries.GetInventoryMovements;
public class GetInventoryMovementsQueryHandler : IRequestHandler<GetInventoryMovementQuery, Result<List<InventoryMovementDto?>>>
{
    private readonly IInventoryMovementRepository _repository;

    public GetInventoryMovementsQueryHandler(IInventoryMovementRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<InventoryMovementDto?>>> Handle(GetInventoryMovementQuery request, CancellationToken cancellationToken)
    {
        var movements = await _repository.GetAllByMovementType(request.MovementType);
        var list = movements
         .Select(m => new InventoryMovementDto(
             m.ProductId,
             (int)m.MovementType,
             m.ReferenceId,
             m.Quantity,
             m.StockBefore,
             m.StockAfter
         ))
         .ToList();
        return Result<List<InventoryMovementDto>>.Success(list)!;
    }
}