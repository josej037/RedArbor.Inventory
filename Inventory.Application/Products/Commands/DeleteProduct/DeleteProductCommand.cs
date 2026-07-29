using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(int Id) : IRequest<Result<bool>>;
