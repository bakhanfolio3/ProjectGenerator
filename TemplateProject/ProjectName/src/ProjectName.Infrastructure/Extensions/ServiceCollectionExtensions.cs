using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using ProjectName.Infrastructure.CacheRepositories;
using ProjectName.Application.Abstraction.CacheRepositories;
using ProjectName.Application.Abstraction.Security;
using ProjectName.Application.Common.Security;
using ProjectName.Application.Abstraction.Services;
using ProjectName.Infrastructure.Services;

namespace ProjectName.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    //public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    //    services.AddScoped<IApplicationDbContext, WriteDbContext>();
    //}

    public static void AddRepositories(this IServiceCollection services)
    {
        #region Repositories

        services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddTransient(typeof(ITenantRepository), typeof(TenantRepository));
        services.AddTransient<ITenantCacheRepository, TenantCacheRepository>();
        services.AddScoped(typeof(IPasswordHasher), typeof(PasswordHasher));
        services.AddTransient(typeof(IAuthenticationRepository), typeof(AuthenticationRepository));
        services.AddTransient(typeof(IUserProfileService), typeof(UserProfileService));

   

        #endregion Repositories
    }
}
