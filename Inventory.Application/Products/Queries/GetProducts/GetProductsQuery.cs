using Inventory.Application.Products.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Products.Queries.GetProducts;

public sealed record GetProductsQuery : IRequest<Result<List<ProductDto>>>;
