using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Abstraction.Responses;
using ProjectName.Application.Abstraction.Security;
using ProjectName.Application.Abstraction.Shared;
using ProjectName.Application.Common.Responses;
using ProjectName.Application.Common.ThrowR;
using ProjectName.Application.DTOs.Identity;
using ProjectName.Application.DTOs.Mail;
using ProjectName.Application.DTOs.Settings;
using ProjectName.Domain.Entities.Identity;
using ProjectName.Domain.Entities.Tenant;
using ProjectName.Infrastructure.DbContext;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.Repositories;
public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly IRepositoryAsync<Authentication> _repository;
    private readonly IRepositoryAsync<User> _userRepository;
    private readonly IRepositoryAsync<Role> _roleRepository;
    private readonly JWTSettings _jwtSettings;
    private readonly IDistributedCache _distributedCache;
    private readonly IPasswordHasher _passwordHasher;

    public AuthenticationRepository(IRepositoryAsync<Authentication> repository, 
        IDistributedCache distributedCache, IPasswordHasher passwordHasher,
        IRepositoryAsync<User> userRepository, IRepositoryAsync<Role> roleRepository,
        IOptions<JWTSettings> jwtSettings) //: base(writeDbContext, readDbContext)
    {
        _distributedCache = distributedCache;
        _repository = repository;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _jwtSettings = jwtSettings.Value;
    }

    public IQueryable<Authentication> Entities => _repository.Entities;

    public async Task<IResult<TokenResponse>> LoginAsync(TokenRequest request, string ipAddress)
    {
        return await GetTokenAsync(request, ipAddress);
    }

    private async Task<IResult<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress)
    {
        var auth = await Entities
            .Include(auth => auth.User)
                .ThenInclude(usr => usr.UserRole)
            .Where(auth => auth.Email == request.Email
                && auth.IsActive && !auth.User.IsDeleted)
            .FirstOrDefaultAsync();

        Throw.Exception.IfNull(auth, nameof(auth), $"No Accounts Registered with {request.Email}.");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var result = _passwordHasher.Check(auth.PasswordHash, request.Password);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        Throw.Exception.IfFalse(auth.EmailConfirmed, $"Email is not confirmed for '{request.Email}'.");
        Throw.Exception.IfFalse(auth.IsActive, $"Account for '{request.Email}' is not active. Please contact the Administrator.");
        Throw.Exception.IfFalse(result, $"Invalid Credentials for '{request.Email}'.");
        JwtSecurityToken jwtSecurityToken = await GenerateJWToken(auth, ipAddress);
        var response = new TokenResponse() 
        { 
            Id = auth.User.Id.ToString(), 
            Email = auth.Email, 
            Username = auth.Email
        };
        response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        response.IssuedOn = jwtSecurityToken.ValidFrom.ToLocalTime();
        response.ExpiresOn = jwtSecurityToken.ValidTo.ToLocalTime();
        response.Role = auth.User.UserRole?.Name ?? string.Empty;
        response.IsVerified = auth.EmailConfirmed;
        var refreshToken = GenerateRefreshToken(ipAddress);
        response.RefreshToken = refreshToken.Token;
        return Result<TokenResponse>.Success(response, "Authenticated");
    }


    private async Task<JwtSecurityToken> GenerateJWToken(Authentication auth, string ipAddress)
    {
        var user = auth.User;
        Throw.Exception.IfNull(auth, "User", $"No User Found");
        var role = user?.UserRole;
        var roleClaim = new List<Claim>();
        if(role != null)
            roleClaim.Add(new Claim(ClaimTypes.Role, role.Name));

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, auth.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, auth.Email),
                new Claim("uid", user?.Id.ToString() ?? string.Empty),
                new Claim("first_name", user?.FirstName ?? string.Empty),
                new Claim("last_name", user?.LastName ?? string.Empty),
                new Claim("full_name", $"{user?.FirstName} {user?.LastName}"),
                new Claim("ip", ipAddress)
            }
        //.Union(userClaims)
        .Union(roleClaim);
        return JWTGeneration(claims);
    }

    //private async Task<UserToken> CreateToken(User user, string deviceid, int? appId = null, int? locationId = null, List<string> groupNames = null, TimeSpan? expiration = null)
    //{
    //    if (appId == null) appId = (int)Application.CattleView;
    //    if (locationId == null && appId == (int)Application.CattleView) locationId = user.DefaultLocation;

    //    var query = _context
    //        .UserAppRoles
    //        .Include(u => u.UserApplication)
    //        .Include(u => u.Role)
    //        .Where(x => x.UserApplication.AppID == appId && x.UserApplication.UserId == user.UserID);
    //    if (locationId != null) query = query.Where(x => x.LocationID == locationId.Value);

    //    var roles = await query.Select(p => p.Role.RoleName).ToListAsync();
    //    var profile = new UserProfile
    //    {
    //        Id = user.UserID,
    //        AppId = appId,
    //        Login = user.UserName,
    //        Email = user.email,
    //        Roles = roles.ToArray(),
    //        DeviceId = deviceid,
    //        DefaultLocation = user.DefaultLocation,
    //        ADGroups = groupNames?.ToArray()
    //    };
    //    var tokenExpiration = appId == (int)Application.CattleView || appId == (int)Application.Porkview || appId == (int)Application.CustomerBE ?
    //        (expiration ?? _defaultWebExpiration)
    //        : (appId == (int)Application.CVMobile ? (expiration ?? _defaultMobileExpiration) : (expiration ?? _defaultExpiration));

    //    return new UserToken(TokenKind.Session, profile, tokenExpiration);
    //}

    private JwtSecurityToken JWTGeneration(IEnumerable<Claim> claims)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }

    private string RandomTokenString()
    {
        using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
        var randomBytes = new byte[40];
        rngCryptoServiceProvider.GetBytes(randomBytes);
        // convert random bytes to hex string
        return BitConverter.ToString(randomBytes).Replace("-", "");
    }

    private RefreshToken GenerateRefreshToken(string ipAddress)
    {
        return new RefreshToken
        {
            Token = RandomTokenString(),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };
    }

    //public async Task<Result<string>> RegisterAsync(RegisterRequest request, string origin)
    //{
    //    var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
    //    if (userWithSameUserName != null)
    //    {
    //        throw new ApiException($"Username '{request.UserName}' is already taken.");
    //    }
    //    var user = new ApplicationUser
    //    {
    //        Email = request.Email,
    //        FirstName = request.FirstName,
    //        LastName = request.LastName,
    //        UserName = request.UserName
    //    };
    //    var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
    //    if (userWithSameEmail == null)
    //    {
    //        var result = await _userManager.CreateAsync(user, request.Password);
    //        if (result.Succeeded)
    //        {
    //            await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
    //            var verificationUri = await SendVerificationEmail(user, origin);
    //            //TODO: Attach Email Service here and configure it via appsettings
    //            await _mailService.SendAsync(new MailRequest() { From = "mail@codewithmukesh.com", To = user.Email, Body = $"Please confirm your account by <a href='{verificationUri}'>clicking here</a>.", Subject = "Confirm Registration" });
    //            return Result<string>.Success(user.Id, message: $"User Registered. Confirmation Mail has been delivered to your Mailbox. (DEV) Please confirm your account by visiting this URL {verificationUri}");
    //        }
    //        else
    //        {
    //            throw new ApiException($"{result.Errors}");
    //        }
    //    }
    //    else
    //    {
    //        throw new ApiException($"Email {request.Email} is already registered.");
    //    }
    //}

    //private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
    //{
    //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
    //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
    //    var route = "api/identity/confirm-email/";
    //    var _enpointUri = new Uri(string.Concat($"{origin}/", route));
    //    var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
    //    verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
    //    //Email Service Call Here
    //    return verificationUri;
    //}

    //public async Task<Result<string>> ConfirmEmailAsync(string userId, string code)
    //{
    //    var user = await _userManager.FindByIdAsync(userId);
    //    code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
    //    var result = await _userManager.ConfirmEmailAsync(user, code);
    //    if (result.Succeeded)
    //    {
    //        return Result<string>.Success(user.Id, message: $"Account Confirmed for {user.Email}. You can now use the /api/identity/token endpoint to generate JWT.");
    //    }
    //    else
    //    {
    //        throw new ApiException($"An error occured while confirming {user.Email}.");
    //    }
    //}

    //public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
    //{
    //    var account = await _userManager.FindByEmailAsync(model.Email);

    //    // always return ok response to prevent email enumeration
    //    if (account == null) return;

    //    var code = await _userManager.GeneratePasswordResetTokenAsync(account);
    //    var route = "api/identity/reset-password/";
    //    var _enpointUri = new Uri(string.Concat($"{origin}/", route));
    //    var emailRequest = new MailRequest()
    //    {
    //        Body = $"You reset token is - {code}",
    //        To = model.Email,
    //        Subject = "Reset Password",
    //    };
    //    //await _mailService.SendAsync(emailRequest);
    //}

    //public async Task<Result<string>> ResetPassword(ResetPasswordRequest model)
    //{
    //    var account = await _userManager.FindByEmailAsync(model.Email);
    //    if (account == null) throw new ApiException($"No Accounts Registered with {model.Email}.");
    //    var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
    //    if (result.Succeeded)
    //    {
    //        return Result<string>.Success(model.Email, message: $"Password Resetted.");
    //    }
    //    else
    //    {
    //        throw new ApiException($"Error occured while reseting the password.");
    //    }
    //}
}

