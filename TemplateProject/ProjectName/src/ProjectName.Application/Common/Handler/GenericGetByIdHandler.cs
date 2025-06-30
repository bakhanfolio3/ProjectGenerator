using ProjectName.Application.Abstraction.CacheRepositories;
using ProjectName.Application.Abstraction.Responses;
using ProjectName.Application.Common.Query;
using ProjectName.Application.Common.Responses;
using ProjectName.Application.Features.Tenant;
using ProjectName.Domain.Entities.Common;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.Common.Handler;
public abstract class GenericGetByIdHandler<TEntity, TRepository, TResponse> : IRequestHandler<GetByIdQuery<TResponse>, IResult<TResponse>>
    where TEntity : class, IEntity
    where TResponse : class, IResponse
    where TRepository : IGenericCacheRepository<TEntity>
{
    public GenericGetByIdHandler(ILogger logger, TRepository cacheRepository)
    {
        Logger = logger;
        CacheRepository = cacheRepository;
    }

    public ILogger Logger { get; }
    public TRepository CacheRepository { get; }

    public async Task<IResult<TResponse>> Handle(GetByIdQuery<TResponse> request, CancellationToken cancellationToken)
    {
        var response = await CacheRepository.GetByIdAsync(request.Id, cancellationToken);
        return Result<TResponse>.Success(response.Adapt<TResponse>());
    }
}
