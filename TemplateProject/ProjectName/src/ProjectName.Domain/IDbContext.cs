using ProjectName.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Domain;
public interface IDbContext: 
    IInfrastructure<IServiceProvider>,
    IDbContextDependencies,
    IDbSetCache,
    IDbContextPoolable
{
    DatabaseFacade Database { get; }
    ChangeTracker ChangeTracker { get; }
    IModel Model { get; }
    DbContextId ContextId { get; }
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    DbSet<TEntity> Set<TEntity>(string name) where TEntity : class;
    int SaveChanges();
    int SaveChanges(bool acceptAllChangesOnSuccess);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default);
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    EntityEntry Entry(object entity);
    EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        where TEntity : class;
    ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(
        TEntity entity,
        CancellationToken cancellationToken = default)
        where TEntity : class;
    EntityEntry<TEntity> Attach<TEntity>(TEntity entity)
        where TEntity : class;
    EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        where TEntity : class;
    EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        where TEntity : class;
    EntityEntry Add(object entity);
    ValueTask<EntityEntry> AddAsync(
        object entity,
        CancellationToken cancellationToken = default);
    EntityEntry Attach(object entity);
    EntityEntry Update(object entity);
    EntityEntry Remove(object entity);
    void AddRange(params object[] entities);
    Task AddRangeAsync(params object[] entities);
    void AttachRange(params object[] entities);
    void UpdateRange(params object[] entities);
    void RemoveRange(params object[] entities);
    void AddRange(IEnumerable<object> entities);
    Task AddRangeAsync(
        IEnumerable<object> entities,
        CancellationToken cancellationToken = default);
    void AttachRange(IEnumerable<object> entities);
    void UpdateRange(IEnumerable<object> entities);
    void RemoveRange(IEnumerable<object> entities);
    object? Find(
        Type entityType,
        params object?[]? keyValues);
    ValueTask<object?> FindAsync(
        Type entityType,
        params object?[]? keyValues);
    ValueTask<object?> FindAsync(
        Type entityType,
        object?[]? keyValues,
        CancellationToken cancellationToken);
    TEntity? Find<TEntity>(
        params object?[]? keyValues)
        where TEntity : class;
    ValueTask<TEntity?> FindAsync<
        TEntity>(params object?[]? keyValues)
        where TEntity : class;
    ValueTask<TEntity?> FindAsync<TEntity>(
        object?[]? keyValues,
        CancellationToken cancellationToken)
        where TEntity : class;
    IQueryable<TResult> FromExpression<TResult>(Expression<Func<IQueryable<TResult>>> expression);



    
}
