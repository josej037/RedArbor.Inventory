using Inventory.Application.Categories.Commands.UpdateCategory;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(CreateCategoryRequest Category) : IRequest<Result<int>>;
