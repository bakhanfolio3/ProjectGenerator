using ProjectName.Application.Abstraction.Responses;
using ProjectName.Application.Common.Responses;
using ProjectName.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.CacheRepositories;
public interface IGenericCacheRepository<TEntity> where TEntity : class, IEntity
{
    bool DisableCache { get; set; }
    Task<List<NameValueResponse<TType>>> GetNVListAsync<TNVEntity, TType>(
        string? searchText,
        CancellationToken cancellationToken = default)
        where TNVEntity : class, TEntity, INameValueEntity<TType>;

    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IListResult<TEntity>> GetCachedListAsync(Expression<Func<TEntity, bool>> predicate,
        string? queryString = null, bool doNotUseCache = false, 
        CancellationToken cancellationToken = default);

    Task<IPaginatedResult<TEntity>> GetCachedListAsync(int pageNumber, int pageSize,
        Expression<Func<TEntity, bool>> predicate,
        string? queryString = null, bool doNotUseCache = false,
        CancellationToken cancellationToken = default);
}
