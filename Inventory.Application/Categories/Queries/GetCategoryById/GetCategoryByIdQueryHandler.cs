using Inventory.Application.Categories.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto?>>
{
    private readonly ICategoryRepository _repository;
    public GetCategoryByIdQueryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<CategoryDto?>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetById(request.Id);
        if (category is null)
        {
            return Result<CategoryDto?>.Failure(new InventoryError(
                    "Category.NotFound",
                    "Category not found"));
        }
        return Result<CategoryDto?>.Success(new CategoryDto(
            category.Id,
            category.Name,
            category.Description,
            category.Active));
    }
}
