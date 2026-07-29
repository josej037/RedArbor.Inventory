using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Categories.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(int Id, UpdateCategoryRequest Request) : IRequest<Result<bool>>;


