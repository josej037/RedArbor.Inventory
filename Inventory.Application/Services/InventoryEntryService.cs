using Inventory.Application.DTOs.InventoryEntry;
using Inventory.Application.Interfaces;
using Inventory.Application.Services.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;

namespace Inventory.Application.Services;

public class InventoryEntryService : IInventoryEntryService
{
    private readonly IInventoryEntryRepository _entryRepo;
    private readonly IInventoryEntryDetailRepository _detailRepo;
    private readonly IProductRepository _productRepo;
    private readonly IInventoryMovementRepository _movementRepo;
    public InventoryEntryService(IInventoryEntryRepository entryRepo, IInventoryEntryDetailRepository detailRepo, IProductRepository productRepo, IInventoryMovementRepository movementRepo)
    {
        _entryRepo = entryRepo;
        _detailRepo = detailRepo;
        _productRepo = productRepo;
        _movementRepo = movementRepo;
    }

    /// <summary>
    /// List all inventory entries.
    /// </summary>
    /// <returns>IEnumerable<InventoryEntryResponse></returns>
    public async Task<IEnumerable<InventoryEntryResponse>> GetAll()
    {
        var entries = await _entryRepo.GetAll();
        return entries.Select(entry => new InventoryEntryResponse
        {
            Id = entry.Id,
            Supplier = entry.Supplier,
            InvoiceNumber = entry.InvoiceNumber,
            ReceivedDate = entry.ReceivedDate
        });
    }

    /// <summary>
    /// Get an inventory entry by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>InventoryEntryResponse</returns>
    public async Task<InventoryEntryResponse?> GetById(int id)
    {
        var entry = await _entryRepo.GetById(id);
        if (entry is null)
            return null;

        return new InventoryEntryResponse
        {
            Id = entry.Id,
            Supplier = entry.Supplier,
            InvoiceNumber = entry.InvoiceNumber,
            ReceivedDate = entry.ReceivedDate
        };
    }

    /// <summary>
    /// Creates a new inventory entry.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>InventoryEntryResponse</returns>
    public async Task<InventoryEntryResponse> Create(InventoryEntryRequest request)
    {
        var entry = new InventoryEntry
        {
            Supplier = request.Supplier,
            InvoiceNumber = request.InvoiceNumber,
            ReceivedDate = request.ReceivedDate
        };
        entry.Id = await _entryRepo.Create(entry);

        foreach (var detail in request.Details)
        {
            var product = await _productRepo.GetById(detail.ProductId);
            if (product is null)
                throw new Exception("Product not found");

            //Create Detail Entry
            var entryDetail = new InventoryEntryDetail
            {
                InventoryEntryId = entry.Id,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                UnitCost = detail.UnitCost
            };
            await _detailRepo.Create(entryDetail);

            //Update Product Stock
            var stockBefore = product.Stock;
            product.Stock += detail.Quantity;
            await _productRepo.Update(product);

            //Create Inventory Movement
            var movement = new InventoryMovement
            {
                ProductId = product.Id,
                MovementType = MovementType.Entry,
                ReferenceId = entry.Id,
                Quantity = detail.Quantity,
                StockBefore = stockBefore,
                StockAfter = product.Stock
            };

            await _movementRepo.Create(movement);
        }

        return new InventoryEntryResponse
        {
            Id = entry.Id,
            Supplier = entry.Supplier,
            InvoiceNumber = entry.InvoiceNumber,
            ReceivedDate = entry.ReceivedDate
        };
    }

    /// <summary>
    /// Updates an existing inventory entry.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task Update(int id, InventoryEntryRequest request)
    {
        var entry = await _entryRepo.GetById(id);
        if (entry is null)
            throw new Exception("Inventory entry not found");
        entry.Supplier = request.Supplier;
        entry.InvoiceNumber = request.InvoiceNumber;
        entry.ReceivedDate = request.ReceivedDate;
        entry.UpdatedAt = DateTime.UtcNow;
        await _entryRepo.Update(entry);
    }

    /// <summary>
    /// Delete an inventory entry.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task Delete(int id)
    {
        var entry = await _entryRepo.GetById(id);
        if (entry is null)
            throw new Exception("Inventory entry not found");
        await _entryRepo.Delete(id);
    }
}
