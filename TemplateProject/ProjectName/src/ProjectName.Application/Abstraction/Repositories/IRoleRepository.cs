using ProjectName.Domain.Entities;
using ProjectName.Domain.Entities.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Repositories;

public interface IRoleRepository : IRepositoryAsync<Role>
{
    //IQueryable<UserRole> UserRoles { get; }

    //Task<List<UserRole>> GetListAsync();

    //Task<UserRole> GetByIdAsync(int userRoleId);

    //Task<int> InsertAsync(UserRole userRole);

    //Task UpdateAsync(UserRole userRole);

    //Task DeleteAsync(UserRole userRole);
}