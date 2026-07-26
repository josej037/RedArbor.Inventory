using Inventory.Application.DTOs.InventoryExit;
using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Moq;

namespace Inventory.Tests;

public class InventoryExitServiceTests
{
    private readonly Mock<IInventoryExitRepository> _exitRepositoryMock;
    private readonly Mock<IInventoryExitDetailRepository    > _detailRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IInventoryMovementRepository> _movementRepositoryMock;

    private readonly InventoryExitService _service;

    public InventoryExitServiceTests()
    {
        _exitRepositoryMock = new Mock<IInventoryExitRepository>();
        _detailRepositoryMock = new Mock<IInventoryExitDetailRepository    >();
        _productRepositoryMock = new Mock<IProductRepository>();
        _movementRepositoryMock = new Mock<IInventoryMovementRepository>();
        _service = new InventoryExitService(
            _exitRepositoryMock.Object,
            _detailRepositoryMock.Object,
            _productRepositoryMock.Object,
            _movementRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnListOfInventoryExits()
    {
        // Arrange
        var inventoryExits = new List<InventoryExit>
        {
            new InventoryExit { Id = 1, Client = "ABC Inc.", OrderNumber = "CBA-001", DeliveredDate = DateTime.UtcNow, Active = true, InventoryExitDetails = new List<InventoryExitDetail>(){new InventoryExitDetail { ProductId = 1, Quantity = 10, UnitCost = 100.00m } } },
            new InventoryExit { Id = 2, Client = "DEF Inc.", OrderNumber = "FED-002", DeliveredDate = DateTime.UtcNow, Active = true, InventoryExitDetails = new List<InventoryExitDetail>(){new InventoryExitDetail { ProductId = 1, Quantity = 5, UnitCost = 200.00m } } }
        };
        _exitRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(inventoryExits);
        // Act
        var result = await _service.GetAll();
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        _exitRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnInventoryExit()
    {
        // Arrange
        var exit = new InventoryExit { Id = 1, Client = "ABC Inc.", OrderNumber = "CBA-001", DeliveredDate = DateTime.UtcNow, Active = true, InventoryExitDetails = new List<InventoryExitDetail>() { new InventoryExitDetail { ProductId = 1, Quantity = 10, UnitCost = 100.00m } } };
        _exitRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(exit);
        // Act
        var result = await _service.GetById(1);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        _exitRepositoryMock.Verify(repo => repo.GetById(1), Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnNull_WhenInventoryExitNotFound()
    {
        // Arrange
        _exitRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync((InventoryExit?)null);
        // Act
        var result = await _service.GetById(1);
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Create_ReturnCreatedInventoryExit()
    {
        var exitDate = DateTime.UtcNow;

        var request = new InventoryExitRequest
        {
            Client = "DEF Inc.",
            OrderNumber = "FED-001",
            DeliveredDate = exitDate,
            Details = new List<InventoryExitDetailRequest>
                {
                    new InventoryExitDetailRequest
                    {
                        ProductId = 1,
                        Quantity = 10,
                        UnitCost = 100.00m
                    }
                }
            };

        var product = new Product
        {
            Id = 1,
            Name = "Laptop",
            Stock = 20,
            Active = true
        };

        _exitRepositoryMock
            .Setup(x => x.Create(It.IsAny<InventoryExit>()))
            .ReturnsAsync(1);

        _productRepositoryMock
            .Setup(x => x.GetById(1))
            .ReturnsAsync(product);

        _detailRepositoryMock
            .Setup(x => x.Create(It.IsAny<InventoryExitDetail>()))
            .ReturnsAsync(1);

        _productRepositoryMock
            .Setup(x => x.Update(It.IsAny<Product>()))
            .Returns(Task.CompletedTask);

        _movementRepositoryMock
            .Setup(x => x.Create(It.IsAny<InventoryMovement>()))
            .ReturnsAsync(1);

        // Act
        var result = await _service.Create(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("DEF Inc.", result.Client);
        Assert.Equal("FED-001", result.OrderNumber);

        _exitRepositoryMock.Verify(
            x => x.Create(It.Is<InventoryExit>(e =>
                e.Client == "DEF Inc." &&
                e.OrderNumber == "FED-001")),
            Times.Once);

        _detailRepositoryMock.Verify(
            x => x.Create(It.IsAny<InventoryExitDetail>()),
            Times.Once);

        _productRepositoryMock.Verify(
            x => x.Update(It.Is<Product>(p =>
                p.Id == 1 &&
                p.Stock == 10)),
            Times.Once);

        _movementRepositoryMock.Verify(
            x => x.Create(It.Is<InventoryMovement>(m =>
                m.ProductId == 1 &&
                m.Quantity == 10 &&
                m.StockBefore == 20 &&
                m.StockAfter == 10)),
            Times.Once);
    }

    [Fact]
    public async Task Updated_ReturnUpdatedInventoryExit()
    {
        // Arrange
        var request = new InventoryExitRequest { Client = "CBA Inc.", OrderNumber = "CBA-100", DeliveredDate = DateTime.UtcNow, Details = new List<InventoryExitDetailRequest>() { new InventoryExitDetailRequest { ProductId = 1, Quantity = 10, UnitCost = 100.00m } } };
        var inventoryExit = new InventoryExit { Client = "ABC Inc.", OrderNumber = "ABC-001", DeliveredDate = DateTime.UtcNow, Active = true, InventoryExitDetails = new List<InventoryExitDetail>() { new InventoryExitDetail { ProductId = 1, Quantity = 10, UnitCost = 100.00m } } };
        _exitRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(inventoryExit);
        _exitRepositoryMock.Setup(repo => repo.Update(It.IsAny<InventoryExit>())).Returns(Task.CompletedTask);
        // Act
        await _service.Update(1, request);
        // Assert
        Assert.Equal("CBA Inc.", inventoryExit.Client);
        Assert.Equal("CBA-100", inventoryExit.OrderNumber);
        _exitRepositoryMock.Verify(repo => repo.Update(inventoryExit), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnDeletedInventoryExit()
    {
        // Arrange
        var inventoryExit = new InventoryExit { Id = 1, Client = "ABC Inc.", OrderNumber = "CBA-001", DeliveredDate = DateTime.UtcNow, Active = true, InventoryExitDetails = new List<InventoryExitDetail>() { new InventoryExitDetail { ProductId = 1, Quantity = 10, UnitCost = 100.00m } } };
        _exitRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(inventoryExit);
        _exitRepositoryMock.Setup(repo => repo.Delete(1)).Returns(Task.CompletedTask);
        // Act
        await _service.Delete(1);
        // Assert
        _exitRepositoryMock.Verify(repo => repo.Delete(1), Times.Once);
    }

}