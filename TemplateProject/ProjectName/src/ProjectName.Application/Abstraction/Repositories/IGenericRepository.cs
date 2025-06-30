using ProjectName.Application.Abstraction.Responses;
using ProjectName.Application.Common.Responses;
using ProjectName.Domain.Entities.Common;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Repositories;
public interface IGenericRepository<TEntity> where TEntity : class, IEntity
{
    public IQueryable<TEntity> Entity => Repository.Entities;

    public IRepositoryAsync<TEntity> Repository { get; }
    public IDistributedCache DistributedCache { get; }

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetAllAsync();

    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IListResult<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<IPaginatedResult<TEntity>> GetListAsync(int pageNumber, int pageSize,
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<List<NameValueResponse<TType>>> GetNVListAsync<TNVEntity, TType>(
        string? searchText,
        CancellationToken cancellationToken = default)
        where TNVEntity : class, TEntity, INameValueEntity<TType>;

    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
}
