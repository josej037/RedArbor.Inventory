using Inventory.Application.Categories.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Categories.Queries.GetCategories;
public sealed record GetCategoriesQuery : IRequest<Result<List<CategoryDto>>>;
