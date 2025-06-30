using ProjectName.Application.Abstraction.CacheRepositories;
using ProjectName.Application.Abstraction.Messagings;
using ProjectName.Application.Abstraction.Responses;
using ProjectName.Application.Common.Query;
using ProjectName.Application.Common.Responses;
using ProjectName.Domain.Entities.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.Common.Handler;
public class GenericNVListQueryHandler<TNVEntity, TCacheRepository, TType> : IListQueryHandler<NVListQuery<TType>, NameValueResponse<TType>>
    where TNVEntity : class, IEntity, INameValueEntity<TType>
    where TCacheRepository : IGenericCacheRepository<TNVEntity>
{
    public GenericNVListQueryHandler(ILogger logger, TCacheRepository cacheRepository)
    {
        Logger = logger;
        CacheRepository = cacheRepository;
    }

    public ILogger Logger { get; }
    public TCacheRepository CacheRepository { get; }

    public async Task<IListResult<NameValueResponse<TType>>> Handle(NVListQuery<TType> request, CancellationToken cancellationToken = default)
    {
        var responses = await CacheRepository.GetNVListAsync<TNVEntity, TType>(request.SearchText, cancellationToken);
        return ListResult<NameValueResponse<TType>>.Success(responses);
    }
}
