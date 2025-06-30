using ProjectName.Application.Abstraction.Responses;
using ProjectName.Application.Common.Responses;
using ProjectName.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Repositories;

public interface IRepositoryAsync<TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> Entities { get; }

    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<List<TEntity>> GetAllAsync();
    Task<List<NameValueResponse<TType>>> GetNVListAsync<TNVEntity, TType>(
        //Expression<Func<TNVEntity, NameValueResponse<TType>>> selector,
        Expression<Func<TNVEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        where TNVEntity : class, TEntity, INameValueEntity<TType>;

    Task<IListResult<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<IPaginatedResult<TEntity>> GetListAsync(int pageNumber, int pageSize,
        Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task Update(TEntity entity);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}