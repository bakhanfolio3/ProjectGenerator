using ProjectName.Application.Abstraction.CacheRepositories;
using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Abstraction.Responses;
using ProjectName.Application.Common.Responses;
using ProjectName.Application.Common.ThrowR;
using ProjectName.Application.Extensions;
using ProjectName.Domain.Entities.Common;
using Mapster;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ProjectName.Infrastructure.CacheRepositories;
public abstract class GenericCacheRepository<TEntity> : IGenericCacheRepository<TEntity> 
    where TEntity : class, IEntity
{
    

    private readonly IDistributedCache _distributedCache;
    private readonly IGenericRepository<TEntity> _repository;

    public GenericCacheRepository(IDistributedCache distributedCache, IGenericRepository<TEntity> repository)
    {
        _distributedCache = distributedCache;
        _repository = repository;
    }

    public bool DisableCache { get; set; }

    public abstract string GetCacheGetKey(int id);
    public abstract string GetCacheListKey(string queryString);
    public abstract string GetCacheNVListKey(string queryString);

    public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        string cacheKey = GetCacheGetKey(id);
        var entity = await _distributedCache.GetAsync<TEntity>(cacheKey, cancellationToken);
        if (entity == null)
        {
            entity = await _repository.GetByIdAsync(id);
            Throw.Exception.IfNull(entity, "Entity", "No Entity Found");
            await _distributedCache.SetAsync(cacheKey, entity);
        }
        return entity;
    }

    public async Task<IListResult<TEntity>> GetCachedListAsync(Expression<Func<TEntity, bool>> predicate,
        string? queryString = null, bool doNotUseCache = false, CancellationToken cancellationToken = default)
    {
        return await GetCachedListAsync(0,0, predicate, queryString, doNotUseCache, cancellationToken);
    }

    public async Task<IPaginatedResult<TEntity>> GetCachedListAsync(int pageNumber, int pageSize,
        Expression<Func<TEntity, bool>> predicate,
        string? queryString = null, bool doNotUseCache = false,
        CancellationToken cancellationToken = default)
    {
        IPaginatedResult<TEntity> entityList;
        if (doNotUseCache || DisableCache)
        {
            entityList = await _repository.GetListAsync(pageNumber, pageSize, predicate, cancellationToken);
        }
        else
        {
            queryString = GetQueryString(pageNumber, pageSize, queryString);
            string cacheKey = GetCacheListKey(queryString);
            entityList = await _distributedCache.GetAsync<PaginatedResult<TEntity>>(cacheKey, token: cancellationToken);
            if (entityList == null)
            {
                entityList = await _repository.GetListAsync(pageNumber, pageSize, predicate, cancellationToken);
                await _distributedCache.SetAsync(cacheKey, entityList, token: cancellationToken);
            }
        }
        return entityList;
    }

    public async Task<List<NameValueResponse<TType>>> GetNVListAsync<TNVEntity, TType>(string? searchText, 
        CancellationToken cancellationToken)
        where TNVEntity : class, TEntity, INameValueEntity<TType>
    {
        string cacheKey = GetCacheNVListKey(string.IsNullOrEmpty(searchText) ? "" : $"searchText={searchText}");
        var entityList = await _distributedCache.GetAsync<List<NameValueResponse<TType>>>(cacheKey, token: cancellationToken);
        if (entityList == null)
        {
            entityList = await _repository.GetNVListAsync<TNVEntity, TType>(searchText, cancellationToken);
            await _distributedCache.SetAsync(cacheKey, entityList, token: cancellationToken);
        }
        return entityList;
    }

    //private string GetQueryString(Expression<Func<TEntity, bool>> predicate)
    //{
    //    return GetQueryString(0, 0, predicate);
    //}
    //// Generate query string from expression
    //private string GetQueryString(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate)
    //{
    //    var query = predicate.Body.ToString();
    //    if (pageSize > 0 && pageNumber > 0)
    //    {
    //        query += $"pageNumber={pageNumber}&pageSize={pageSize}";
    //    }
    //    return HttpUtility.UrlEncode(query);
    //}

    private string GetQueryString(int pageNumber, int pageSize, string? queryString)
    {
        var query = queryString ?? "";
        if (pageSize > 0 && pageNumber > 0)
        {
            query += $"pageNumber={pageNumber}&pageSize={pageSize}";
        }
        return query;
    }
}
