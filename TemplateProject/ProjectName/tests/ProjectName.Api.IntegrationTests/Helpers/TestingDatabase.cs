using ProjectName.Application.Common;
using System;
using System.Collections.Generic;
using ProjectName.Domain.Entities.Enums;
using ProjectName.Domain;
using ProjectName.Domain.Entities.Identity;
using ProjectName.Application.Abstraction.Contexts;

namespace ProjectName.Api.IntegrationTests.Helpers;

public static class TestingDatabase
{
    public static async Task SeedDatabase(Func<IApplicationDbContext> contextFactory)
    {
        await using var db = contextFactory();
        db.Users.AddRange(GetSeedingHeroes);
        await db.SaveChangesAsync();
    }


    public static readonly List<User> GetSeedingHeroes =
        new()
        {
            new(){ Id = 1, FirstName = "Corban Best"  },
            new() { Id = 2, FirstName = "Priya Hull"},
            new() { Id = 3, FirstName = "Harrison Vu" }
        };
}