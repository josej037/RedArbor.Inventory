using Inventory.Application.Interfaces;
using Inventory.Application.Products.Commands.CreateProduct;
using Inventory.Application.Products.Commands.DeleteProduct;
using Inventory.Application.Products.Commands.UpdateProduct;
using Inventory.Application.Products.Queries.GetProductById;
using Inventory.Application.Products.Queries.GetProducts;
using Inventory.Domain.Entities;
using Moq;

namespace Inventory.Tests;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _repository;

    private readonly GetProductsQueryHandler _getAllHandler;
    private readonly GetProductByIdQueryHandler _getByIdHandler;
    private readonly CreateProductCommandHandler _createHandler;
    private readonly UpdateProductCommandHandler _updateHandler;
    private readonly DeleteProductCommandHandler _deleteHandler;
    public ProductServiceTests()
    {
        _repository = new Mock<IProductRepository>();

        _getAllHandler = new GetProductsQueryHandler(_repository.Object);
        _getByIdHandler = new GetProductByIdQueryHandler(_repository.Object);
        _createHandler = new CreateProductCommandHandler(_repository.Object);
        _updateHandler = new UpdateProductCommandHandler(_repository.Object);
        _deleteHandler = new DeleteProductCommandHandler(_repository.Object);
    }

    [Fact]
    public async Task GetAll_ReturnListOfProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Refrigerator", Description = "14-cubic-foot refrigerator", Price = 100.00m, Stock=10, CategoryId = 1, Active = true },
            new Product { Id = 2, Name = "Gas stove", Description = "4-burner gas stove", Price = 200.00m, Stock=5, CategoryId = 1, Active = true }
        };

        _repository.Setup(x => x.GetAll()).ReturnsAsync(products);

        // Act
        var result = await _getAllHandler.Handle(new GetProductsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Value!.Count());

        _repository.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnProduct()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Refrigerator", Description = "14-cubic-foot refrigerator", Price = 100.00m, Stock = 5, CategoryId = 1, Active = true };
        _repository.Setup(repo => repo.GetById(1)).ReturnsAsync(product);
        // Act
        var result = await _getByIdHandler.Handle(new GetProductByIdQuery(1), CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Value!.Id);
    }

    [Fact]
    public async Task GetById_ReturnNull_WhenProductNotFound()
    {
        // Arrange
        _repository.Setup(x => x.GetById(1)).ReturnsAsync((Product?)null);
        // Act
        var result = await _getByIdHandler.Handle(new GetProductByIdQuery(1), CancellationToken.None);
        // Assert
        Assert.True(result.IsFailure);
    }

    [Fact]
    public async Task Create_ReturnCreatedProduct()
    {
        // Arrange
        var request = new CreateProductRequest { Name = "Gas stove", Description = "6-burner gas stove", Price = 300.00m, Stock = 5, CategoryId = 1 };
        var product = new Product { Id = 2, Name = "Gas stove", Description = "4-burner gas stove", Price = 200.00m, Stock = 5, CategoryId = 1, Active = true };
        _repository.Setup(repo => repo.Create(It.IsAny<Product>())).ReturnsAsync(1);
        // Act
        var result = await _createHandler.Handle(new CreateProductCommand(request), CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Value!);
        _repository.Verify(repo => repo.Create(It.Is<Product>(
             p => p.Name == "Gas stove" &&
             p.Description == "6-burner gas stove" &&
             p.Price == 300.00m)
        ), Times.Once);
    }

    [Fact]
    public async Task Updated_ReturnUpdatedProduct()
    {
        // Arrange
        var request = new UpdateProductRequest { Name = "Gas stove", Description = "6-burner gas stove", Price = 300.00m };
        var product = new Product { Id = 1, Name = "Gas stove", Description = "4-burner gas stove", Price = 200.00m, Active = true };
        _repository.Setup(repo => repo.GetById(1)).ReturnsAsync(product);
        _repository.Setup(repo => repo.Update(It.IsAny<Product>())).Returns(Task.CompletedTask);
        // Act
        var result = await _updateHandler.Handle(
            new UpdateProductCommand(1, request),
            CancellationToken.None);
        // Assert
        Assert.Equal("Gas stove", product.Name);
        Assert.Equal("6-burner gas stove", product.Description);
        _repository.Verify(repo => repo.Update(product), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnDeletedProduct()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Gas stove", Description = "6-burner gas stove", Price = 300.00m, Active = true };
        _repository.Setup(repo => repo.GetById(1)).ReturnsAsync(product);
        _repository.Setup(repo => repo.Delete(1)).Returns(Task.CompletedTask);
        // Act
        var result = await _deleteHandler.Handle(new DeleteProductCommand(1), CancellationToken.None);

        // Assert
        _repository.Verify(repo => repo.Delete(1), Times.Once);
    }

}