using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

    Task CommitTransactionAsync(CancellationToken cancellationToken);

    IDbContextTransaction BeginTransaction();

    void CommitTransaction();

    void Rollback();
    Task RollbackAsync(CancellationToken cancellationToken);
}