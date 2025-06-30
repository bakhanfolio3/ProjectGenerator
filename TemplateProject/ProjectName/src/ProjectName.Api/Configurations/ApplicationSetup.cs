using ProjectName.Application.Abstraction.Contexts;
using ProjectName.Application.Common;
using ProjectName.Application.DTOs.Settings;
using ProjectName.Domain;
using ProjectName.Infrastructure.DbContext;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectName.Api.Configurations;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplicationSetup(this IServiceCollection services)
    {
        //services.AddScoped<WriteDbContext>();
        services.AddScoped<IWriteDbContext, WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();
        //NewId.SetProcessIdProvider(new CurrentProcessIdProvider());
        //services.AddSingleton<JWTSettings>();

        return services;
    }


}