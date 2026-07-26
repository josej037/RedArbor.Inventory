using Inventory.Application.DTOs.InventoryEntry;
using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Moq;

namespace Inventory.Tests;

public class InventoryEntryServiceTests
{
    private readonly Mock<IInventoryEntryRepository> _entryRepositoryMock;
    private readonly Mock<IInventoryEntryDetailRepository> _detailRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IInventoryMovementRepository> _movementRepositoryMock;

    private readonly InventoryEntryService _service;

    public InventoryEntryServiceTests()
    {
        _entryRepositoryMock = new Mock<IInventoryEntryRepository>();
        _detailRepositoryMock = new Mock<IInventoryEntryDetailRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _movementRepositoryMock = new Mock<IInventoryMovementRepository>();
        _service = new InventoryEntryService(
            _entryRepositoryMock.Object,
            _detailRepositoryMock.Object,
            _productRepositoryMock.Object,
            _movementRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnListOfInventoryEntries()
    {
        // Arrange
        var inventoryEntries = new List<InventoryEntry>
        {
            new InventoryEntry { Supplier = "ABC Inc.", InvoiceNumber = "CBA-001", ReceivedDate = DateTime.UtcNow, Active = true, InventoryEntryDetails = new List<InventoryEntryDetail>(){new InventoryEntryDetail { ProductId = 1, Quantity = 10, UnitCost = 100.00m } } },
            new InventoryEntry { Supplier = "DEF Inc.", InvoiceNumber = "FED-002", ReceivedDate = DateTime.UtcNow, Active = true, InventoryEntryDetails = new List<InventoryEntryDetail>(){new InventoryEntryDetail { ProductId = 1, Quantity = 5, UnitCost = 200.00m } } }
        };
        _entryRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(inventoryEntries);
        // Act
        var result = await _service.GetAll();
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        _entryRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnInventoryEntry()
    {
        // Arrange
        var entry = new InventoryEntry { Id = 1, Supplier = "ABC Inc.", InvoiceNumber = "CBA-001", ReceivedDate = DateTime.UtcNow, Active = true, InventoryEntryDetails = new List<InventoryEntryDetail>() { new InventoryEntryDetail { ProductId = 1, Quantity = 10, UnitCost = 100.00m } } };
        _entryRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(entry);
        // Act
        var result = await _service.GetById(1);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        _entryRepositoryMock.Verify(repo => repo.GetById(1), Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnNull_WhenInventoryEntryNotFound()
    {
        // Arrange
        _entryRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync((InventoryEntry?)null);
        // Act
        var result = await _service.GetById(1);
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Create_ReturnCreatedInventoryEntry()
    {
        var receivedDate = DateTime.UtcNow;

        var request = new InventoryEntryRequest
        {
            Supplier = "DEF Inc.",
            InvoiceNumber = "FED-001",
            ReceivedDate = receivedDate,
            Details = new List<InventoryEntryDetailRequest>
        {
            new InventoryEntryDetailRequest
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

        _entryRepositoryMock
            .Setup(x => x.Create(It.IsAny<InventoryEntry>()))
            .ReturnsAsync(1);

        _productRepositoryMock
            .Setup(x => x.GetById(1))
            .ReturnsAsync(product);

        _detailRepositoryMock
            .Setup(x => x.Create(It.IsAny<InventoryEntryDetail>()))
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
        Assert.Equal("DEF Inc.", result.Supplier);
        Assert.Equal("FED-001", result.InvoiceNumber);

        _entryRepositoryMock.Verify(
            x => x.Create(It.Is<InventoryEntry>(e =>
                e.Supplier == "DEF Inc." &&
                e.InvoiceNumber == "FED-001")),
            Times.Once);

        _detailRepositoryMock.Verify(
            x => x.Create(It.IsAny<InventoryEntryDetail>()),
            Times.Once);

        _productRepositoryMock.Verify(
            x => x.Update(It.Is<Product>(p =>
                p.Id == 1 &&
                p.Stock == 30)),
            Times.Once);

        _movementRepositoryMock.Verify(
            x => x.Create(It.Is<InventoryMovement>(m =>
                m.ProductId == 1 &&
                m.Quantity == 10 &&
                m.StockBefore == 20 &&
                m.StockAfter == 30)),
            Times.Once);
    }

    [Fact]
    public async Task Updated_ReturnUpdatedInventoryEntry()
    {
        // Arrange
        var request = new InventoryEntryRequest { Supplier = "CBA Inc.", InvoiceNumber = "CBA-100", ReceivedDate = DateTime.UtcNow, Details = new List<InventoryEntryDetailRequest>() { new InventoryEntryDetailRequest { ProductId = 1, Quantity = 10, UnitCost = 100.00m } } };
        var inventoryEntry = new InventoryEntry { Supplier = "ABC Inc.", InvoiceNumber = "ABC-001", ReceivedDate = DateTime.UtcNow, Active = true, InventoryEntryDetails = new List<InventoryEntryDetail>() { new InventoryEntryDetail { ProductId = 1, Quantity = 10, UnitCost = 100.00m } } };
        _entryRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(inventoryEntry);
        _entryRepositoryMock.Setup(repo => repo.Update(It.IsAny<InventoryEntry>())).Returns(Task.CompletedTask);
        // Act
        await _service.Update(1, request);
        // Assert
        Assert.Equal("CBA Inc.", inventoryEntry.Supplier);
        Assert.Equal("CBA-100", inventoryEntry.InvoiceNumber);
        _entryRepositoryMock.Verify(repo => repo.Update(inventoryEntry), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnDeletedInventoryEntry()
    {
        // Arrange
        var inventoryEntry = new InventoryEntry { Id = 1, Supplier = "ABC Inc.", InvoiceNumber = "CBA-001", ReceivedDate = DateTime.UtcNow, Active = true, InventoryEntryDetails = new List<InventoryEntryDetail>() { new InventoryEntryDetail { ProductId = 1, Quantity = 10, UnitCost = 100.00m } } };
        _entryRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(inventoryEntry);
        _entryRepositoryMock.Setup(repo => repo.Delete(1)).Returns(Task.CompletedTask);
        // Act
        await _service.Delete(1);
        // Assert
        _entryRepositoryMock.Verify(repo => repo.Delete(1), Times.Once);
    }

}