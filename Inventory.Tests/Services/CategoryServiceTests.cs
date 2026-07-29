using Inventory.Application.Categories.Commands.CreateCategory;
using Inventory.Application.Categories.Commands.DeleteCategory;
using Inventory.Application.Categories.Commands.UpdateCategory;
using Inventory.Application.Categories.Queries.GetCategories;
using Inventory.Application.Categories.Queries.GetCategoryById;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Moq;

namespace Inventory.Tests;

public class CategoryServiceTests
{
    private readonly Moq.Mock<ICategoryRepository> _Repository;
    private readonly GetCategoriesQueryHandler _handlerList;
    private readonly GetCategoryByIdQueryHandler _handlerByIdQ;
    private readonly CreateCategoryCommandHandler _handlerCreate;
    private readonly UpdateCategoryCommandHandler _handlerUpdate;
    private readonly DeleteCategoryCommandHandler _handlerDelete;

    public CategoryServiceTests()
    {
        _Repository = new Mock<ICategoryRepository>();
        _handlerList = new GetCategoriesQueryHandler(_Repository.Object);
        _handlerByIdQ = new GetCategoryByIdQueryHandler(_Repository.Object);
        _handlerCreate = new CreateCategoryCommandHandler(_Repository.Object);
        _handlerUpdate = new UpdateCategoryCommandHandler(_Repository.Object);
        _handlerDelete = new DeleteCategoryCommandHandler(_Repository.Object);
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
        var result = await _handlerList.Handle(
             new GetCategoriesQuery(),
             CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result!.Value!.Count);

        _Repository.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnCategory()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Home", Description = "Home & Furniture", Active = true };
        _Repository.Setup(repo => repo.GetById(1)).ReturnsAsync(category);
        // Act
        var result = await _handlerByIdQ.Handle(
             new GetCategoryByIdQuery(1),
             CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Value!.Id);
    }

    [Fact]
    public async Task GetById_ReturnNull_WhenCategoryNotFound()
    {
        // Arrange
        _Repository.Setup(repo => repo.GetById(1)).ReturnsAsync((Category?)null);
        // Act
        var result = await _handlerByIdQ.Handle(
             new GetCategoryByIdQuery(1),
             CancellationToken.None);
        // Assert
        Assert.True(result.IsFailure);
    }

    [Fact]
    public async Task Create_ReturnCreatedCategory()
    {
        // Arrange
        var request = new CreateCategoryRequest { Name = "Home", Description = "Home & Furniture" };
        var category = new Category { Id = 1, Name = "Home", Description = "Home & Furniture", Active = true };
        _Repository.Setup(repo => repo.Create(It.IsAny<Category>())).ReturnsAsync(1);
        // Act
        var result = await _handlerCreate.Handle(
             new CreateCategoryCommand(request),
             CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Value);
        _Repository.Verify(repo => repo.Create(It.Is<Category>(
             c => c.Name == "Home" &&
             c.Description == "Home & Furniture")
        ), Times.Once);
    }

    [Fact]
    public async Task Updated_ReturnUpdatedCategory()
    {
        // Arrange
        var request = new UpdateCategoryRequest { Name = "living room", Description = "living rooms" };
        var category = new Category { Id = 1, Name = "Home", Description = "Home & Furniture", Active = true };
        _Repository.Setup(repo => repo.GetById(1)).ReturnsAsync(category);
        _Repository.Setup(repo => repo.Update(It.IsAny<Category>())).Returns(Task.CompletedTask);
        // Act
        await _handlerUpdate.Handle(
             new UpdateCategoryCommand(1, request),
             CancellationToken.None);
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
        await _handlerDelete.Handle(
             new DeleteCategoryCommand(1),
             CancellationToken.None);
        // Assert
        _Repository.Verify(repo => repo.Delete(1), Times.Once);
    }

}