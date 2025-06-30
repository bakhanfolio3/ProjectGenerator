using ProjectName.Application.Abstraction.Contexts;
using ProjectName.Domain.Entities.Identity;
using ProjectName.Domain.Entities.Tenant;
using ProjectName.Infrastructure.Configuration.Write;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace ProjectName.Infrastructure.DbContext;

public class WriteDbContext : Microsoft.EntityFrameworkCore.DbContext, IWriteDbContext//  IdentityDbContext<ApplicationUser, ApplicationRole, int>, IContext
{
    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options) { }
    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<Authentication> Authentications { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly, WriteConfigurationFilter);
    }

    private static bool WriteConfigurationFilter(Type type) =>
        type.FullName?.Contains("Configuration.Write") ?? false;
}