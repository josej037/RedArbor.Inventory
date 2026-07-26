using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Moq;

namespace Inventory.Tests;

public class InventoryMovementServiceTests
{
    private readonly Mock<IInventoryMovementRepository> _Repository;
    private readonly InventoryMovementService _service;

    public InventoryMovementServiceTests()
    {
        _Repository = new Mock<IInventoryMovementRepository>();
        _service = new InventoryMovementService(_Repository.Object);
    }

    [Fact]
    public async Task GetAll_ReturnListOfInventoryMovementsEntry()
    {
        // Arrange
        var inventoryMovements = new List<InventoryMovement>
        {
            new InventoryMovement { Id = 1, ProductId = 1, MovementType = MovementType.Entry, ReferenceId = 1, Quantity = 10, StockBefore = 0, StockAfter = 10, Active = true, Product = new Product { Id = 1, Name = "Screen", Description = "50-inch 4K screen", Price = 100, Stock = 10 } },
            new InventoryMovement { Id = 2, ProductId = 1, MovementType = MovementType.Entry, ReferenceId = 2, Quantity = 5, StockBefore = 10, StockAfter = 15, Active = true, Product = new Product { Id = 1, Name = "Screen", Description = "50-inch 4K screen", Price = 100, Stock = 15 } }
        };
        _Repository.Setup(repo => repo.GetAllByMovementType(It.IsAny<MovementType>())).ReturnsAsync(inventoryMovements);
        // Act
        var result = await _service.GetAllByMovementType((int)MovementType.Entry);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        _Repository.Verify(repo => repo.GetAllByMovementType(It.IsAny<MovementType>()), Times.Once);
    }


    [Fact]
    public async Task GetAll_ReturnListOfInventoryMovementsExit()
    {
        // Arrange
        var inventoryMovements = new List<InventoryMovement>
        {
            new InventoryMovement { Id = 1, ProductId = 1, MovementType = MovementType.Exit, ReferenceId = 1, Quantity = 10, StockBefore = 10, StockAfter = 0, Active = true, Product = new Product { Id = 1, Name = "Screen", Description = "50-inch 4K screen", Price = 100, Stock = 10 } },
            new InventoryMovement { Id = 2, ProductId = 1, MovementType = MovementType.Exit, ReferenceId = 2, Quantity = 5, StockBefore = 10, StockAfter = 5, Active = true, Product = new Product { Id = 1, Name = "Screen", Description = "50-inch 4K screen", Price = 100, Stock = 5 } }
        };
        _Repository.Setup(repo => repo.GetAllByMovementType(It.IsAny<MovementType>())).ReturnsAsync(inventoryMovements);
        // Act
        var result = await _service.GetAllByMovementType((int)MovementType.Exit);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        _Repository.Verify(repo => repo.GetAllByMovementType(It.IsAny<MovementType>()), Times.Once);
    }
}