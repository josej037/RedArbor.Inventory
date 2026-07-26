using Inventory.Application.DTOs.InventoryMovement;
using Inventory.Application.DTOs.Product;
using Inventory.Application.Interfaces;
using Inventory.Application.Services.Interfaces;
using Inventory.Domain.Enums;

namespace Inventory.Application.Services;

public class InventoryMovementService : IInventoryMovementService
{
    private readonly IInventoryMovementRepository _repository;
    public InventoryMovementService(IInventoryMovementRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// List all inventory movements by type.
    /// </summary>
    /// <param name="MovementType"></param>
    /// <returns>IEnumerable<InventoryMovementResponse></returns>
    public async Task<IEnumerable<InventoryMovementResponse>> GetAllByMovementType(int MovementType)
    {
        var movementType = (MovementType)MovementType;
        var movements = await _repository.GetAllByMovementType(movementType);
        return movements.Select(movement => new InventoryMovementResponse
        {
            ProductId = movement.ProductId,
            MovementType = (int)movement.MovementType,
            ReferenceId = movement.ReferenceId,
            Quantity = movement.Quantity,
            StockBefore = movement.StockBefore,
            StockAfter = movement.StockAfter,
            Product = new ProductResponse
            {
                Id = movement.Product.Id,
                Name = movement.Product.Name,
                Description = movement.Product.Description,
                Price = movement.Product.Price,
                Stock = movement.Product.Stock
            }
        });
    }
}