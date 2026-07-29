using Inventory.Application.Interfaces;
using Inventory.Application.InventoryMovements.Queries.GetInventoryMovements;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Moq;

namespace Inventory.Tests;

public class InventoryMovementServiceTests
{
    private readonly Mock<IInventoryMovementRepository> _repository;
    private readonly GetInventoryMovementsQueryHandler _handler;

    public InventoryMovementServiceTests()
    {
        _repository = new Mock<IInventoryMovementRepository>();
        _handler = new GetInventoryMovementsQueryHandler(_repository.Object);
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
        _repository.Setup(x => x.GetAllByMovementType(MovementType.Entry)).ReturnsAsync(inventoryMovements);
        // Act
        var result = await _handler.Handle(new GetInventoryMovementQuery(MovementType.Entry), CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Value!.Count());

        _repository.Verify(repo => repo.GetAllByMovementType(It.IsAny<MovementType>()), Times.Once);
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
        _repository.Setup(x => x.GetAllByMovementType(MovementType.Exit)).ReturnsAsync(inventoryMovements);
        // Act
        var result = await _handler.Handle(new GetInventoryMovementQuery(MovementType.Exit), CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Value!.Count());

        Assert.NotNull(result);
        Assert.Equal(2, result.Value!.Count());

        _repository.Verify(repo => repo.GetAllByMovementType(It.IsAny<MovementType>()), Times.Once);
    }
}