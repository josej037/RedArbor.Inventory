using Inventory.Application.DTOs.Category;
using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Moq;

namespace Inventory.Tests;

public class CategoryServiceTests
{
    private readonly Moq.Mock<ICategoryRepository> _Repository;
    private readonly CategoryService _Service;
    public CategoryServiceTests()
    {
        _Repository = new Mock<ICategoryRepository>();
        _Service = new CategoryService(_Repository.Object);
    }

    [Fact]
    public async Task GetAll_ReturnListOfCategories()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Home", Description = "Home & Furniture", Active = true },
            new Category { Id = 2, Name = "Office", Description = "Office supplies", Active = true }
        };
        _Repository.Setup(repo => repo.GetAll()).ReturnsAsync(categories);
        // Act
        var result = await _Service.GetAll();
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        _Repository.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnCategory()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Home", Description = "Home & Furniture", Active = true };
        _Repository.Setup(repo => repo.GetById(1)).ReturnsAsync(category);
        // Act
        var result = await _Service.GetById(1);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetById_ReturnNull_WhenCategoryNotFound()
    {
        // Arrange
        _Repository.Setup(repo => repo.GetById(1)).ReturnsAsync((Category?)null);
        // Act
        var result = await _Service.GetById(1);
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Create_ReturnCreatedCategory()
    {
        // Arrange
        var request = new CategoryRequest { Name = "Home", Description = "Home & Furniture" };
        var category = new Category { Id = 1, Name = "Home", Description = "Home & Furniture", Active = true };
        _Repository.Setup(repo => repo.Create(It.IsAny<Category>())).ReturnsAsync(1);
        // Act
        var result = await _Service.Create(request);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        _Repository.Verify(repo => repo.Create(It.Is<Category>(
             c => c.Name == "Home" &&
             c.Description == "Home & Furniture")
        ), Times.Once);
    }

    [Fact]
    public async Task Updated_ReturnUpdatedCategory()
    {
        // Arrange
        var request = new CategoryRequest { Name = "living room", Description = "living rooms" };
        var category = new Category { Id = 1, Name = "Home", Description = "Home & Furniture", Active = true };
        _Repository.Setup(repo => repo.GetById(1)).ReturnsAsync(category);
        _Repository.Setup(repo => repo.Update(It.IsAny<Category>())).Returns(Task.CompletedTask);
        // Act
        await _Service.Update(1, request);
        // Assert
        Assert.Equal("living room", category.Name);
        Assert.Equal("living rooms", category.Description);
        _Repository.Verify(repo => repo.Update(category), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnDeletedCategory()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Home", Description = "Home & Furniture", Active = true };
        _Repository.Setup(repo => repo.GetById(1)).ReturnsAsync(category);
        _Repository.Setup(repo => repo.Delete(1)).Returns(Task.CompletedTask);
        // Act
        await _Service.Delete(1);
        // Assert
        _Repository.Verify(repo => repo.Delete(1), Times.Once);
    }

}