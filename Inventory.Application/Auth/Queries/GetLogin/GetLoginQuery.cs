using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Auth.Queries.GetLogin;

public sealed record GetLoginQuery(GetLoginRequest Request) : IRequest<Result<GetLoginResponse?>>;
