using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(CreateProductRequest Product) : IRequest<Result<int>>;
