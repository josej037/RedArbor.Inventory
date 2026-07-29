using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Categories.Commands.UpdateCategory;
public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<bool>>
{
    private readonly ICategoryRepository _repository;
    public UpdateCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetById(request.Id);
        if (category is null)
        {
            return Result<bool>.Failure(
                new InventoryError(
                    "Category.NotFound",
                    "The specified category does not exist."));
        }
        category.Name = request.Request.Name;
        category.Description = request.Request.Description;
        category.UpdatedAt = DateTime.UtcNow;
        await _repository.Update(category);
        return Result<bool>.Success(true);
    }
}
