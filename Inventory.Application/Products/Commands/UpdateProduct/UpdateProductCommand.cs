using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(int Id, UpdateProductRequest Request) : IRequest<Result<bool>>;
