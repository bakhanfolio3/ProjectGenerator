using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Domain;
using ProjectName.Domain.Entities.Identity;
using ProjectName.Domain.Entities.Tenant;
using ProjectName.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.Repositories;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly IRepositoryAsync<User> _repository;
    private readonly IDistributedCache _distributedCache;

    public UserRepository(ILogger<UserRepository> logger, IRepositoryAsync<User> repository, 
        IDistributedCache distributedCache) 
        : base(logger, repository, distributedCache)
    {
        _distributedCache = distributedCache;
        _repository = repository;
    }

    public override string GetCacheGetKey(int id)
    {
        throw new NotImplementedException();
    }

    public override string GetCacheListKey()
    {
        throw new NotImplementedException();
    }

    protected override Expression<Func<TNV, bool>> GetNVListPredicate<TNV, TType>(string? searchText)
    {
        throw new NotImplementedException();
    }
}
