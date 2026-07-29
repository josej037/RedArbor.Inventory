using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<bool>>
{
    private readonly IProductRepository _repository;

    public UpdateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetById(request.Id);
        if (product is null)
        {
            return Result<bool>.Failure(
                new InventoryError(
                    "Product.NotFound",
                    "The specified product does not exist."));
        }
        product.Name = request.Request.Name;
        product.Description = request.Request.Description;
        product.Price = request.Request.Price;
        product.Stock = request.Request.Stock;
        product.CategoryId = request.Request.CategoryId;
        product.UpdatedAt = DateTime.UtcNow;
        await _repository.Update(product);

        return Result<bool>.Success(true);
    }
}
