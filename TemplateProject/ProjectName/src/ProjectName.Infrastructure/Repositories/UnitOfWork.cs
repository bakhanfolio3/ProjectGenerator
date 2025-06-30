using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Abstraction.Security;
using ProjectName.Application.Abstraction.Services;
using ProjectName.Application.Abstraction.Shared;
using ProjectName.Application.DTOs.Identity;
using ProjectName.Domain.Entities.Common;
using ProjectName.Infrastructure.DbContext;
using ProjectName.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    //private readonly IAuthenticatedUserService _authenticatedUserService;
    private readonly WriteDbContext _dbContext;
    private readonly IUserProfileService _userProfileService;
    private bool disposed;
    protected IUserProfile? UserProfile { get; }
    public ILogger Logger { get; }

    public UnitOfWork(ILogger<UnitOfWork> logger, WriteDbContext dbContext, IUserProfileService userProfileService)//, IAuthenticatedUserService authenticatedUserService
    {
        Logger = logger;
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this._userProfileService = userProfileService;
        UserProfile = userProfileService.GetLoggedInUserProfile();
        //_authenticatedUserService = authenticatedUserService;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await SaveChangesAsync(false, true, cancellationToken);
    }

    public async Task<int> SaveChangesAsync(bool bypassSoftDelete = false, bool writeTrackableDate = true, CancellationToken cancellationToken = default)
    {
        if (!bypassSoftDelete)
            SoftDeleteHelper.ProcessSoftDelete(_dbContext.ChangeTracker);
        TrackableHelpers.PopulateTrackableFields(
        changes: _dbContext.ChangeTracker,
       username: UserProfile?.Username ?? null, writeTrackableDate);


        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _dbContext.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _dbContext.Database.CommitTransaction();
    }

    public void Rollback()
    {
        _dbContext.Database.RollbackTransaction();
    }

    public Task RollbackAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Database.RollbackTransactionAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                //dispose managed resources
                _dbContext.Dispose();
            }
        }
        //dispose unmanaged resources
        disposed = true;
    }
}
