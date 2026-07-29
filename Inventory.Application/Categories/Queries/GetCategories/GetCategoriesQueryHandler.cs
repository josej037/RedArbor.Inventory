using Inventory.Application.Categories.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Result<List<CategoryDto>>>
{
    private readonly ICategoryRepository _repository;
    public GetCategoriesQueryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAll();
        var list = categories
            .Select(c => new CategoryDto(c.Id, c.Name, c.Description, c.Active))
            .ToList();
        return Result<List<CategoryDto>>.Success(list);
    }

}
