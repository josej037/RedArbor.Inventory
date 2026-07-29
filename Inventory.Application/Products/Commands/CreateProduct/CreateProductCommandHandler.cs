using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
{
    private readonly IProductRepository _repository;
    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product
        {
            Name = request.Product.Name,
            Description = request.Product.Description!,
            Price = request.Product.Price,
            Stock = request.Product.Stock,
            CategoryId = request.Product.CategoryId
        };
        var result = await _repository.Create(product);
        return Result<int>.Success(result);
    }
}
