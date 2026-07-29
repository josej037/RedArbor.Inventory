using Inventory.Application.Interfaces;
using Inventory.Application.Results;
using MediatR;

namespace Inventory.Application.Auth.Queries.GetLogin;

public class GetLoginQueryHandler : IRequestHandler<GetLoginQuery, Result<GetLoginResponse?>>
{
    private readonly IUserRepository _repository;
    private readonly IJwtToken _jwtToken;

    public GetLoginQueryHandler(IUserRepository repository, IJwtToken jwtToken)
    {
        _repository = repository;
        _jwtToken = jwtToken;
    }

    public async Task<Result<GetLoginResponse?>> Handle(GetLoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.Login(request.Request.Name);
        if (user is null)
        {
            return Result<GetLoginResponse?>.Failure(new InventoryError(
                    "User.NotFound",
                    "User or password is incorrect"));
        }

        if (user.Password != request.Request.Password)
            return Result<GetLoginResponse?>.Failure(new InventoryError(
                "User.IncorrectPassword",
                "User or password is incorrect"));

        var token = _jwtToken.Generate(user);


        return Result<GetLoginResponse?>.Success(new GetLoginResponse(
            token,
            DateTime.UtcNow.AddMinutes(60)));
    }
}
