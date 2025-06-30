using ProjectName.Application.Abstraction.CacheRepositories;
using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Common.Responses;
using ProjectName.Application.Common.ThrowR;
using ProjectName.Application.Extensions;
using ProjectName.Domain.Entities.Tenant;
using ProjectName.Infrastructure.CacheKeys;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.CacheRepositories;
public class TenantCacheRepository : GenericCacheRepository<Tenant>, ITenantCacheRepository
{
    public TenantCacheRepository(IDistributedCache distributedCache, ITenantRepository repository)
        : base(distributedCache, repository)
    {
    }

    public override string GetCacheGetKey(int id) => TenantCacheKeys.GetKey(id);

    public override string GetCacheListKey(string queryString) => TenantCacheKeys.GetListKey(queryString);

    public override string GetCacheNVListKey(string queryString) => TenantCacheKeys.GetNVListKey(queryString);
}
