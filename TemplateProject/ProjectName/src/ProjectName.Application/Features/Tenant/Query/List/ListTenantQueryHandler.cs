//using ProjectName.Application.Features.Users.CreateUser;
using System;
using Microsoft.EntityFrameworkCore;
using ProjectName.Application.Abstraction.CacheRepositories;
using System.Linq.Expressions;
using ProjectName.Application.Common.Handler;
using ProjectName.Application.Features.Tenant.Query.Get;
using Microsoft.Extensions.Logging;

namespace ProjectName.Application.Features.Tenant.Query.List;
public class ListTenantQueryHandler : GenericPaginatedQueryHandler<Domain.Entities.Tenant.Tenant, ITenantCacheRepository, TenantResponse, ListTenantQuery> 
{
    public ListTenantQueryHandler(ILogger<ListTenantQueryHandler> logger, ITenantCacheRepository cacheRepository)
        :base(logger, cacheRepository)
    {
    }
    protected override Expression<Func<Domain.Entities.Tenant.Tenant, bool>> GetListPredicate(ListTenantQuery request) => 
        x => EF.Functions.Like(x.Name, $"%{request.SearchText}%");
}
