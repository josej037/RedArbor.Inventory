using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<int>>
{
    private readonly ICategoryRepository _repository;
    public CreateCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Domain.Entities.Category
        {
            Name = request.Category.Name,
            Description = request.Category.Description!
        };
        var result = await _repository.Create(category);
        return Result<int>.Success(result);
    }
}
