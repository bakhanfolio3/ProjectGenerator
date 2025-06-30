using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Repositories;
public interface ITenantRepository : IGenericRepository<Domain.Entities.Tenant.Tenant>
{
}
