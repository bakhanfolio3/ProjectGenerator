using ProjectName.Domain.Entities.Common;
using System;

namespace ProjectName.Domain.Auth.Interfaces;

public interface ISession
{
    public int UserId { get; }

    public DateTime Now { get; }
}