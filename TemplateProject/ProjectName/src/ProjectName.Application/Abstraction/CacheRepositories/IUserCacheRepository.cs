using ProjectName.Domain.Entities;
using ProjectName.Domain.Entities.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.CacheRepositories;

public interface IUserCacheRepository
{
    Task<List<User>> GetCachedListAsync();

    Task<User> GetByIdAsync(int brandId);
}
