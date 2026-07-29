using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<bool>>
{
    private readonly ICategoryRepository _repository;
    public DeleteCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetById(request.Id);
        if (category is null)
        {
            return Result<bool>.Failure(
                new InventoryError(
                    "Category.NotFound",
                    "The specified category does not exist."));
        }
        await _repository.Delete(request.Id);
        return Result<bool>.Success(true);
    }
}
