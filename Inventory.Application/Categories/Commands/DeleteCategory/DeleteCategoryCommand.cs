using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Categories.Commands.DeleteCategory;

public sealed record DeleteCategoryCommand(int Id) : IRequest<Result<bool>>;
