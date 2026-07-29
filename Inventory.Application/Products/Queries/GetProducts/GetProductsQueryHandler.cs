using Inventory.Application.Interfaces;
using Inventory.Application.Products.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Products.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<List<ProductDto>>>
{
    private readonly IProductRepository _repository;

    public GetProductsQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAll();
        var list = products
            .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId, p.Active))
            .ToList();

        return Result<List<ProductDto>>.Success(list);
    }

}
