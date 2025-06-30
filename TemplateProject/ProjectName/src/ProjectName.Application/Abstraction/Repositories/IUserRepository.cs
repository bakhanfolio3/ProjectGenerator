using ProjectName.Domain.Entities;
using ProjectName.Domain.Entities.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Repositories;
public interface IUserRepository : IGenericRepository<User>
{
    //IQueryable<User> Users { get; }

    //Task<List<User>> GetListAsync();

    //Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken = default);

    //Task<int> InsertAsync(User user);

    //Task UpdateAsync(User user);

    //Task DeleteAsync(User user);
}