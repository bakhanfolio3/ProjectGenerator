using ProjectName.Application.Auth;
using ProjectName.Domain.Auth.Interfaces;
using ProjectName.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EntityFramework.Exceptions.MySQL.Pomelo;
using ProjectName.Infrastructure.DbContext;

namespace ProjectName.Api.Configurations;

public static class PersistenceSetup
{
    public static IServiceCollection AddPersistenceSetup(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<ISession, Session>();
        services.AddHostedService<ApplicationDbInitializer>();
        services.AddDbContextPool<WriteDbContext>(o =>
        {
            o.UseMySql(
                configuration.GetConnectionString("DefaultConnection"), 
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                opts => opts.MigrationsAssembly(typeof(WriteDbContext).Assembly.FullName));
            o.UseExceptionProcessor();
        });
        services.AddDbContextPool<ReadDbContext>(o =>
        {
            o.UseMySql(
                configuration.GetConnectionString("DefaultConnection"), 
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                opts => opts.MigrationsAssembly(typeof(WriteDbContext).Assembly.FullName));
            o.UseExceptionProcessor();
        });

        return services;
    }
}