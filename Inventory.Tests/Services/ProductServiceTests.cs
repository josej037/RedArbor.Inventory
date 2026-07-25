using Inventory.Application.DTOs.Product;
using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Moq;

namespace Inventory.Tests;

public class ProductServiceTests
{
    private readonly Moq.Mock<IProductRepository> _Repository;
    private readonly ProductService _Service;
    public ProductServiceTests()
    {
        _Repository = new Mock<IProductRepository>();
        _Service = new ProductService(_Repository.Object);
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
        _Repository.Setup(repo => repo.GetAll()).ReturnsAsync(products);
        // Act
        var result = await _Service.GetAll();
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        _Repository.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnProduct()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Refrigerator", Description = "14-cubic-foot refrigerator", Price = 100.00m, Stock = 5, CategoryId = 1, Active = true };
        _Repository.Setup(repo => repo.GetById(1)).ReturnsAsync(product);
        // Act
        var result = await _Service.GetById(1);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetById_ReturnNull_WhenProductNotFound()
    {
        // Arrange
        _Repository.Setup(repo => repo.GetById(1)).ReturnsAsync((Product?)null);
        // Act
        var result = await _Service.GetById(1);
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Create_ReturnCreatedProduct()
    {
        // Arrange
        var request = new ProductRequest { Name = "Gas stove", Description = "6-burner gas stove", Price = 300.00m, Stock = 5, CategoryId = 1 };
        var product = new Product { Id = 2, Name = "Gas stove", Description = "4-burner gas stove", Price = 200.00m, Stock = 5, CategoryId = 1, Active = true };
        _Repository.Setup(repo => repo.Create(It.IsAny<Product>())).ReturnsAsync(1);
        // Act
        var result = await _Service.Create(request);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        _Repository.Verify(repo => repo.Create(It.Is<Product>(
             p => p.Name == "Gas stove" &&
             p.Description == "6-burner gas stove" &&
             p.Price == 300.00m)
        ), Times.Once);
    }

    [Fact]
    public async Task Updated_ReturnUpdatedProduct()
    {
        // Arrange
        var request = new ProductRequest { Name = "Gas stove", Description = "6-burner gas stove", Price = 300.00m };
        var product = new Product { Id = 1, Name = "Gas stove", Description = "4-burner gas stove", Price = 200.00m, Active = true };
        _Repository.Setup(repo => repo.GetById(1)).ReturnsAsync(product);
        _Repository.Setup(repo => repo.Update(It.IsAny<Product>())).Returns(Task.CompletedTask);
        // Act
        await _Service.Update(1, request);
        // Assert
        Assert.Equal("Gas stove", product.Name);
        Assert.Equal("6-burner gas stove", product.Description);
        _Repository.Verify(repo => repo.Update(product), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnDeletedProduct()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Gas stove", Description = "6-burner gas stove", Price = 300.00m, Active = true };
        _Repository.Setup(repo => repo.GetById(1)).ReturnsAsync(product);
        _Repository.Setup(repo => repo.Delete(1)).Returns(Task.CompletedTask);
        // Act
        await _Service.Delete(1);
        // Assert
        _Repository.Verify(repo => repo.Delete(1), Times.Once);
    }

}