using ProjectName.Domain.Entities;
using ProjectName.Domain.Entities.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.CacheRepositories;

public interface IRoleCacheRepository
{
    Task<List<Role>> GetCachedListAsync();

    Task<Role> GetByIdAsync(int brandId);
}
