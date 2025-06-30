using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectName.Infrastructure.DbContext;
using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Common.Responses;
using System.Linq.Expressions;
using System;
using ProjectName.Application.Extensions;
using System.Threading;
using ProjectName.Domain.Entities.Common;
using ProjectName.Application.Abstraction.Responses;
using Microsoft.Extensions.Logging;

namespace ProjectName.Infrastructure.Repositories;
public class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : class, IEntity
{
    public RepositoryAsync(ILogger<RepositoryAsync<TEntity>> logger, WriteDbContext writeDbContext, ReadDbContext readDbContext)
    {
        Logger = logger;
        WriteDbContext = writeDbContext;
        ReadDbContext = readDbContext;
    }

    public ILogger Logger { get; }
    public WriteDbContext WriteDbContext { get; }
    public ReadDbContext ReadDbContext { get; }

    public IQueryable<TEntity> Entities => WriteDbContext.Set<TEntity>();
    public IQueryable<TEntity> ReadEntities => ReadDbContext.Set<TEntity>();



    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await WriteDbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entity;
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        WriteDbContext.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await ReadDbContext
            .Set<TEntity>()
            .ToListAsync();
    }

    public async Task<List<NameValueResponse<TType>>> GetNVListAsync<TNVEntity, TType>(
        //Expression<Func<TNVEntity, NameValueResponse<TType>>> selector,
        Expression<Func<TNVEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        where TNVEntity : class, TEntity, INameValueEntity<TType>
    {
        return await ReadDbContext
            .Set<TNVEntity>()
            .Where(predicate)
            .Select(x => new NameValueResponse<TType> { Name = x.Name, Value = x.Value })
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await ReadDbContext.Set<TEntity>().FindAsync(id, cancellationToken);
    }

    private IQueryable<TEntity> CreateQuery(Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, TEntity>>? selector = null, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = ReadDbContext
            .Set<TEntity>().AsQueryable();
        if (selector != null)
            query = query.Select(selector);
        if (predicate != null)
            query = query.Where(predicate);
        if (includes != null && includes.Length > 0)
            for (var i = 0; i < includes.Length; i++)
                query = query.Include(includes[i]);

        return query;
    }

    public async Task<IListResult<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var query = CreateQuery(predicate);
        return ListResult<TEntity>.Success(await query.AsNoTracking().ToListAsync(cancellationToken));
    }

    public async Task<IPaginatedResult<TEntity>> GetListAsync(int pageNumber, int pageSize,
        Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var query = CreateQuery(predicate);
        return await query.AsNoTracking().ToPaginatedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public Task Update(TEntity entity)
    {
        WriteDbContext.Entry(entity).CurrentValues.SetValues(entity);
        return Task.CompletedTask;
        //WriteDbContext.Update(entity, cancellationToken);
    }
}
