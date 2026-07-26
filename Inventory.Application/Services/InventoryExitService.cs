using Inventory.Application.DTOs.InventoryExit;
using Inventory.Application.Interfaces;
using Inventory.Application.Services.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;

namespace Inventory.Application.Services;

public class InventoryExitService : IInventoryExitService
{
    private readonly IInventoryExitRepository _exitRepo;
    private readonly IInventoryExitDetailRepository _detailRepo;
    private readonly IProductRepository _productRepo;
    private readonly IInventoryMovementRepository _movementRepo;
    public InventoryExitService(IInventoryExitRepository exitRepo, IInventoryExitDetailRepository detailRepo, IProductRepository productRepo, IInventoryMovementRepository movementRepo)
    {
        _exitRepo = exitRepo;
        _detailRepo = detailRepo;
        _productRepo = productRepo;
        _movementRepo = movementRepo;
    }

    /// <summary>
    /// List all inventory exits.
    /// </summary>
    /// <returns>IEnumerable<InventoryExitResponse></returns>
    public async Task<IEnumerable<InventoryExitResponse>> GetAll()
    {
        var exits = await _exitRepo.GetAll();
        return exits.Select(exit => new InventoryExitResponse
        {
            Id = exit.Id,
            Client = exit.Client,
            OrderNumber = exit.OrderNumber,
            DeliveredDate = exit.DeliveredDate
        });
    }

    /// <summary>
    /// Get an inventory exit by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>InventoryExitResponse</returns>
    public async Task<InventoryExitResponse?> GetById(int id)
    {
        var exit = await _exitRepo.GetById(id);
        if (exit is null)
            return null;

        return new InventoryExitResponse
        {
            Id = exit.Id,
            Client = exit.Client,
            OrderNumber = exit.OrderNumber,
            DeliveredDate = exit.DeliveredDate
        };
    }

    /// <summary>
    /// Creates a new inventory exit.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>InventoryExitResponse</returns>
    public async Task<InventoryExitResponse> Create(InventoryExitRequest request)
    {
        var exit = new InventoryExit
        {
            Client = request.Client,
            OrderNumber = request.OrderNumber,
            DeliveredDate = request.DeliveredDate
        };
        exit.Id = await _exitRepo.Create(exit);

        foreach (var detail in request.Details)
        {
            var product = await _productRepo.GetById(detail.ProductId);
            if (product is null)
                throw new Exception("Product not found");

            //Create Detail Exit
            var exitDetail = new InventoryExitDetail
            {
                InventoryExitId = exit.Id,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                UnitCost = detail.UnitCost
            };
            await _detailRepo.Create(exitDetail);

            //Update Product Stock
            var stockBefore = product.Stock;
            product.Stock -= detail.Quantity;
            await _productRepo.Update(product);

            //Create Inventory Movement
            var movement = new InventoryMovement
            {
                ProductId = product.Id,
                MovementType = MovementType.Exit,
                ReferenceId = exit.Id,
                Quantity = detail.Quantity,
                StockBefore = stockBefore,
                StockAfter = product.Stock
            };

            await _movementRepo.Create(movement);
        }

        return new InventoryExitResponse
        {
            Id = exit.Id,
            Client = exit.Client,
            OrderNumber = exit.OrderNumber,
            DeliveredDate = exit.DeliveredDate
        };
    }

    /// <summary>
    /// Updates an existing inventory exit.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task Update(int id, InventoryExitRequest request)
    {
        var exit = await _exitRepo.GetById(id);
        if (exit is null)
            throw new Exception("Inventory exit not found");
        exit.Client = request.Client;
        exit.OrderNumber = request.OrderNumber;
        exit.DeliveredDate = request.DeliveredDate;
        exit.UpdatedAt = DateTime.UtcNow;
        await _exitRepo.Update(exit);
    }

    /// <summary>
    /// Delete an inventory exit.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task Delete(int id)
    {
        var exit = await _exitRepo.GetById(id);
        if (exit is null)
            throw new Exception("Inventory exit not found");
        await _exitRepo.Delete(id);
    }
}