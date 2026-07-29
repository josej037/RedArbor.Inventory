using Inventory.Application.Interfaces;
using Inventory.Application.InventoryEntries.Commands.CreateInventoryEntry;
using Inventory.Application.InventoryEntries.Commands.DeleteInventoryEntry;
using Inventory.Application.InventoryEntries.Commands.UpdateInventoryEntry;
using Inventory.Application.InventoryEntries.DTOs;
using Inventory.Application.InventoryEntries.Queries.GetInventoryEntries;
using Inventory.Application.InventoryEntries.Queries.GetInventoryEntryById;
using Inventory.Domain.Entities;
using Moq;

namespace Inventory.Tests;

public class InventoryEntryServiceTests
{
    private readonly Mock<IInventoryEntryRepository> _entryRepositoryMock;
    private readonly Mock<IInventoryEntryDetailRepository> _detailRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IInventoryMovementRepository> _movementRepositoryMock;

    private readonly GetInventoryEntriesQueryHandler _getAllHandler;
    private readonly GetInventoryEntryByIdQueryHandler _getByIdHandler;
    private readonly CreateInventoryEntryCommandHandler _createHandler;
    private readonly UpdateInventoryEntryCommandHandler _updateHandler;
    private readonly DeleteInventoryEntryCommandHandler _deleteHandler;

    public InventoryEntryServiceTests()
    {
        _entryRepositoryMock = new Mock<IInventoryEntryRepository>();
        _detailRepositoryMock = new Mock<IInventoryEntryDetailRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _movementRepositoryMock = new Mock<IInventoryMovementRepository>();

        _getAllHandler = new GetInventoryEntriesQueryHandler(
            _entryRepositoryMock.Object);

        _getByIdHandler = new GetInventoryEntryByIdQueryHandler(
            _entryRepositoryMock.Object);

        _createHandler = new CreateInventoryEntryCommandHandler(
            _entryRepositoryMock.Object, 
            _detailRepositoryMock.Object);

        _updateHandler = new UpdateInventoryEntryCommandHandler(
            _entryRepositoryMock.Object,
            _detailRepositoryMock.Object);

        _deleteHandler = new DeleteInventoryEntryCommandHandler(
            _entryRepositoryMock.Object);
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
        var result = await _getAllHandler.Handle(new GetInventoryEntriesQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(2, result.Value.Count);

        _entryRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnInventoryEntry()
    {
        // Arrange
        var entry = new InventoryEntry { Id = 1, Supplier = "ABC Inc.", InvoiceNumber = "CBA-001", ReceivedDate = DateTime.UtcNow, Active = true, InventoryEntryDetails = new List<InventoryEntryDetail>() { new InventoryEntryDetail { ProductId = 1, Quantity = 10, UnitCost = 100.00m } } };
        _entryRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(entry);
        // Act
        var result = await _getByIdHandler.Handle(new GetInventoryEntryByIdQuery(1), CancellationToken.None);
        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(1, result.Value.Id);
        _entryRepositoryMock.Verify(repo => repo.GetById(1), Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnNull_WhenInventoryEntryNotFound()
    {
        // Arrange
        _entryRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync((InventoryEntry?)null);
        // Act
        var result = await _getByIdHandler.Handle(new GetInventoryEntryByIdQuery(1), CancellationToken.None);
        // Assert
        Assert.Null(result.Value);
    }

    [Fact]
    public async Task Create_ReturnCreatedInventoryEntry()
    {
        var receivedDate = DateTime.UtcNow;

        var request = new InventoryEntryDto(
            1,
            "CBA Inc.",
            "CBA-100",
            DateTime.UtcNow,
            new List<InventoryEntryDetailDto>
            {
                new InventoryEntryDetailDto(
                    1,
                    1,
                    10,
                    100.00m
                )
        });

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

        //_productRepositoryMock
        //    .Setup(x => x.GetById(1))
        //    .ReturnsAsync(product);

        //_detailRepositoryMock
        //    .Setup(x => x.Create(It.IsAny<IEnumerable<CreateInventoryEntryDetailRequest>>()))
        //    .ReturnsAsync(1);

        //_productRepositoryMock
        //    .Setup(x => x.Update(It.IsAny<Product>()))
        //    .Returns(Task.CompletedTask);

        //_movementRepositoryMock
        //    .Setup(x => x.Create(It.IsAny<InventoryMovement>()))
        //    .ReturnsAsync(1);

        // Act
        var result = await _createHandler.Handle(new CreateInventoryEntryCommand(request), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);

        _entryRepositoryMock.Verify(
            x => x.Create(It.Is<InventoryEntry>(e =>
                e.Supplier == "CBA Inc." &&
                e.InvoiceNumber == "CBA-100")),
            Times.Once);
        
        //_detailRepositoryMock.Verify(
        //    x => x.Create(It.IsAny<InventoryEntryDetail>()),
        //    Times.Once);

        //_productRepositoryMock.Verify(
        //    x => x.Update(It.Is<Product>(p =>
        //        p.Id == 1 &&
        //        p.Stock == 30)),
        //    Times.Once);

        //_movementRepositoryMock.Verify(
        //    x => x.Create(It.Is<InventoryMovement>(m =>
        //        m.ProductId == 1 &&
        //        m.Quantity == 10 &&
        //        m.StockBefore == 20 &&
        //        m.StockAfter == 30)),
        //    Times.Once);
    }

    [Fact]
    public async Task Updated_ReturnUpdatedInventoryEntry()
    {
        // Arrange
        var request = new InventoryEntryDto(1, "CBA Inc.", "CBA-100", DateTime.UtcNow, new List<InventoryEntryDetailDto> { new InventoryEntryDetailDto( 1, 1, 10, 100.00m) });
        var inventoryEntry = new InventoryEntry { Id = 1, Supplier = "ABC Inc.", InvoiceNumber = "ABC-001", ReceivedDate = DateTime.UtcNow, Active = true, InventoryEntryDetails = new List<InventoryEntryDetail>() { new InventoryEntryDetail { ProductId = 1, Quantity = 10, UnitCost = 100.00m } } };
        _entryRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(inventoryEntry);
        _entryRepositoryMock.Setup(repo => repo.Update(It.IsAny<InventoryEntry>())).Returns(Task.CompletedTask);
        // Act
        var result = await _updateHandler.Handle(new UpdateInventoryEntryCommand(1, request), CancellationToken.None);
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
        var result = await _deleteHandler.Handle(new DeleteInventoryEntryCommand(1), CancellationToken.None);
        // Assert
        _entryRepositoryMock.Verify(repo => repo.Delete(1), Times.Once);
    }

}