using Inventory.Application.Auth.DTOs;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Auth.Queries.GetLogin;

public sealed record GetLoginQuery(LoginDto Request) : IRequest<Result<TokenDto?>>;
