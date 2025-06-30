using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Common.Handler;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Features.Tenant.Command.Update;
public class UpdateTenantHandler : GenericUpdateHandler<Domain.Entities.Tenant.Tenant, ITenantRepository, TenantResponse, 
    UpdateTenantCommand>
{
    public UpdateTenantHandler(ILogger<UpdateTenantHandler> logger, ITenantRepository repository, 
        IUnitOfWork unitOfWork) 
        : base(logger, repository, unitOfWork)
    {
    }
}

