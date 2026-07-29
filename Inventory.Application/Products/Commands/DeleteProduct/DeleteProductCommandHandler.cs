using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
{
    private readonly IProductRepository _repository;

    public DeleteProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetById(request.Id);
        if (product is null)
        {
            return Result<bool>.Failure(
                new InventoryError(
                    "Product.NotFound",
                    "The specified product does not exist."));
        }

        await _repository.Delete(request.Id);
        return Result<bool>.Success(true);
    }
}
