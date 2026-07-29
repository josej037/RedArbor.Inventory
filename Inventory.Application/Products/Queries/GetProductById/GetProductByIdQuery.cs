using Inventory.Application.Products.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(int Id) : IRequest<Result<ProductDto?>>;
