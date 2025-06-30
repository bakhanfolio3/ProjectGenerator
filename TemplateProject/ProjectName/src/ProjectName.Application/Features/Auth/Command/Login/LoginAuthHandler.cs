using ProjectName.Application.Abstraction.Responses;
using System.Threading;
using System.Threading.Tasks;
using ProjectName.Application.Abstraction.Repositories;
using Mapster;
using ProjectName.Application.Abstraction.Messagings;

namespace ProjectName.Application.Features.Auth.Command.Login;
public class LoginAuthHandler : ICommandHandler<LoginAuthCommand, AuthResponse>
{
    private readonly IAuthenticationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public LoginAuthHandler(IAuthenticationRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<AuthResponse>> Handle(LoginAuthCommand request, CancellationToken cancellationToken)
    {
        var loginResult = await _repository.LoginAsync(new DTOs.Identity.TokenRequest { Email = request.Email, Password = request.Password, TenantId = request.TenantId }, request.IpAddress);
        return loginResult.Adapt<IResult<AuthResponse>>();
    }


}
