﻿//using ProjectName.Domain.Entities.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ProjectName.Application.Common;

//public interface IContext : IAsyncDisposable, IDisposable
//{
//    public DatabaseFacade Database { get; }
    
//    public DbSet<User> Users { get; }
    
//    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
//}