using ProjectName.Application.Abstraction.Security;
using ProjectName.Application.Abstraction.Services;
using ProjectName.Application.DTOs.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.Services;
public class UserProfileService : IUserProfileService
{
    public UserProfileService(IHttpContextAccessor httpContext)
    {
        HttpContext = httpContext?.HttpContext;
    }

    public HttpContext? HttpContext { get; }

    public IUserProfile? GetLoggedInUserProfile()
    {
        if (HttpContext is not null)
        {
            var authToken = HttpContext?.Request?.Headers[HeaderNames.Authorization].FirstOrDefault()?.Split(' ')?.Last();
            //AuthToken = authToken;

            if (!string.IsNullOrEmpty(authToken))
            {
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(authToken);
                var profile = GetUserProfile(jwtToken);
                return profile;
            }
        }
        return null;
    }

    // Get the user profile from the jwtSecurotyToken claims
    public IUserProfile GetUserProfile(JwtSecurityToken jwtToken)
    {
        var profile = new UserProfile()
        {
            Email = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value ?? string.Empty,
            Username = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value ?? string.Empty,
            Role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
            FirstName = jwtToken.Claims.FirstOrDefault(c => c.Type == "first_name")?.Value ?? string.Empty,
            LastName = jwtToken.Claims.FirstOrDefault(c => c.Type == "last_name")?.Value ?? string.Empty,
            Id = jwtToken.Claims.FirstOrDefault(c => c.Type == "uid")?.Value ?? string.Empty
        };
        return profile;
    }


}
