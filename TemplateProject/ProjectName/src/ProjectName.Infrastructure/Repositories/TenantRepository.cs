using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Common.Responses;
using ProjectName.Domain.Entities.Tenant;
using ProjectName.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.Repositories;
public class TenantRepository : GenericRepository<Tenant>,  ITenantRepository
{
    public TenantRepository(ILogger<TenantRepository> logger, IRepositoryAsync<Tenant> repository, IDistributedCache distributedCache)
        : base(logger, repository, distributedCache)
    {
    }

    public override string GetCacheGetKey(int id) => CacheKeys.TenantCacheKeys.GetKey(id);

    public override string GetCacheListKey() => CacheKeys.TenantCacheKeys.ListKey;


    protected override Expression<Func<TNVEntity, bool>> GetNVListPredicate<TNVEntity, TType>(string? searchText)
    {
        return x => string.IsNullOrEmpty(searchText) || searchText.Contains(x.Name);
    }
}
