using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Common.Command;
using ProjectName.Application.Common.Handler;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Features.Tenant.Command.Delete;
public class DeleteTenantHandler : GenericDeleteHandler<Domain.Entities.Tenant.Tenant, ITenantRepository, DeleteCommand>
{
    public DeleteTenantHandler(ILogger<DeleteTenantHandler> logger, ITenantRepository repository, 
        IUnitOfWork unitOfWork) : base(logger, repository, unitOfWork)
    {
    }
}

