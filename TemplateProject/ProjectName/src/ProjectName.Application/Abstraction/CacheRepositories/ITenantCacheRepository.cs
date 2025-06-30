using ProjectName.Domain.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.CacheRepositories;
public interface ITenantCacheRepository : IGenericCacheRepository<Tenant>
{

}
