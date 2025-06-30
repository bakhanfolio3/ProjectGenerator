using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Abstraction.Responses;
using ProjectName.Application.Common.Responses;
using ProjectName.Domain.Entities.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.Repositories;
public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
{

    public GenericRepository(ILogger logger, IRepositoryAsync<TEntity> repository, IDistributedCache distributedCache)
    {
        Logger = logger;
        Repository = repository;
        DistributedCache = distributedCache;
    }

    public IQueryable<TEntity> Entity => Repository.Entities;

    public ILogger Logger { get; }
    public IRepositoryAsync<TEntity> Repository { get; }
    public IDistributedCache DistributedCache { get; }

    public abstract string GetCacheListKey();
    public abstract string GetCacheGetKey(int id);

    public async virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Repository.DeleteAsync(entity);
        await DistributedCache.RemoveAsync(GetCacheListKey(), cancellationToken);
        await DistributedCache.RemoveAsync(GetCacheGetKey(entity.Id), cancellationToken);
    }

    public async virtual Task<List<TEntity>> GetAllAsync()
    {
        return await Repository.GetAllAsync();
    }

    public async virtual Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Repository.GetByIdAsync(id, cancellationToken);
    }

    public async virtual Task<IListResult<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await Repository.GetListAsync(predicate, cancellationToken);
    }

    public async virtual Task<IPaginatedResult<TEntity>> GetListAsync(int pageNumber, int pageSize,
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await Repository.GetListAsync(pageNumber, pageSize, predicate, cancellationToken);
    }


    public async virtual Task<List<NameValueResponse<TType>>> GetNVListAsync<TNVEntity, TType>(
        string? searchText,
        CancellationToken cancellationToken = default)
        where TNVEntity : class, TEntity, INameValueEntity<TType>
    {
        return await Repository.GetNVListAsync<TNVEntity, TType>(GetNVListPredicate<TNVEntity, TType>(searchText), cancellationToken);
    }

    protected abstract Expression<Func<TNV, bool>> GetNVListPredicate<TNV, TType>(string? searchText)
        where TNV : class, IEntity, INameValueEntity<TType>;

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Repository.AddAsync(entity, cancellationToken);
        await DistributedCache.RemoveAsync(GetCacheListKey(), cancellationToken);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Repository.Update(entity);
        await DistributedCache.RemoveAsync(GetCacheListKey(), cancellationToken);
        await DistributedCache.RemoveAsync(GetCacheGetKey(entity.Id), cancellationToken);
        return entity;
    }

}
