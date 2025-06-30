using ProjectName.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using ISession = ProjectName.Domain.Auth.Interfaces.ISession;

namespace ProjectName.Application.Auth;

public class Session : ISession
{
    public int UserId { get; private init; }

    public DateTime Now => DateTime.Now;

    public Session(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        var nameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier);

        if (nameIdentifier != null)
        {
            UserId = 0;
        }
    }

}