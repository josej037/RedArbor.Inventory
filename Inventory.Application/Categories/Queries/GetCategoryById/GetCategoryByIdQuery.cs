using Inventory.Application.Categories.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(int Id) : IRequest<Result<CategoryDto?>>;
