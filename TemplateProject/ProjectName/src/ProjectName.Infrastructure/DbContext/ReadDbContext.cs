using ProjectName.Application.Abstraction.Contexts;
using ProjectName.Domain.Entities.Identity;
using ProjectName.Domain.Entities.Tenant;
using ProjectName.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace ProjectName.Infrastructure.DbContext;

public class ReadDbContext : Microsoft.EntityFrameworkCore.DbContext, IReadDbContext//  IdentityDbContext<ApplicationUser, ApplicationRole, int>, IContext
{
    public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Authentication> Authentications { get; set; } = null!;
    public DbSet<Tenant> Tenants { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ReadDbContext).Assembly, ReadConfigurationFilter);
    }

    private static bool ReadConfigurationFilter(Type type) => 
        type.FullName?.Contains("Configuration.Read") ?? false;
}