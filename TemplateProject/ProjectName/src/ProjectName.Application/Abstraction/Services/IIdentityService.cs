using ProjectName.Application.Common.Responses;
using ProjectName.Application.DTOs.Identity;
using ProjectName.Application.Abstraction.Responses;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Services;

public interface IIdentityService
{
    Task<IResult<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);

    Task<IResult<string>> RegisterAsync(RegisterRequest request, string origin);

    Task<IResult<string>> ConfirmEmailAsync(string userId, string code);

    Task ForgotPassword(ForgotPasswordRequest model, string origin);

    Task<IResult<string>> ResetPassword(ResetPasswordRequest model);
}