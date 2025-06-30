using ProjectName.Application.Abstraction.CacheRepositories;
using ProjectName.Application.Abstraction.Messagings;
using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Abstraction.Responses;
using ProjectName.Application.Common.Handler;
using ProjectName.Application.Common.Query;
using ProjectName.Application.Common.Responses;
using ProjectName.Application.Features.Tenant.Query.List;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.Features.Tenant.Query.NVList;
public class NVListTenantQueryHandler : GenericNVListQueryHandler<Domain.Entities.Tenant.Tenant, ITenantCacheRepository, int>
{
    public NVListTenantQueryHandler(ILogger<NVListTenantQueryHandler> logger, ITenantCacheRepository cacheRepository) 
        : base(logger, cacheRepository)
    {
    }
}
