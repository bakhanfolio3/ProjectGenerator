using ProjectName.Domain;
using ProjectName.Domain.Entities.Identity;
using ProjectName.Domain.Entities.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Contexts;

    public interface IApplicationDbContext : IDbContext
    {
    //IDbConnection Connection { get; }
    //bool HasChanges { get; }

    //EntityEntry Entry(object entity);

    //Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DbSet<Authentication> Authentications { get; set; }

    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }

    DbSet<Tenant> Tenants { get; set; }
}
