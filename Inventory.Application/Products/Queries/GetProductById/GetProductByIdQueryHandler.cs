using Inventory.Application.Interfaces;
using Inventory.Application.Products.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto?>>
{
    private readonly IProductRepository _repository;
    public GetProductByIdQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProductDto?>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetById(request.Id);
        if (product is null)
        {
            return Result<ProductDto?>.Failure(new InventoryError(
                    "Product.NotFound",
                    "Product not found"));
        }
        return Result<ProductDto?>.Success(new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock,
            product.CategoryId,
            product.Active));
    }
}
