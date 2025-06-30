using ProjectName.Application.Abstraction.CacheRepositories;
using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Abstraction.Responses;
using ProjectName.Application.Common.Handler;
using ProjectName.Application.Common.Query;
using ProjectName.Application.Common.Responses;
using ProjectName.Application.Features.Users;
using ProjectName.Domain.Entities.Tenant;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.Features.Tenant.Query.Get;
public class GetTenantByIdHandler : GenericGetByIdHandler<Domain.Entities.Tenant.Tenant, ITenantCacheRepository, TenantResponse> //IRequestHandler<GetByIdQuery<TenantResponse>, IResult<TenantResponse>>
{
    public GetTenantByIdHandler(ILogger<GetTenantByIdHandler> logger, ITenantCacheRepository cacheRepository) 
        : base(logger, cacheRepository)
    {
    }
}
