using ProjectName.Application.Abstraction.Responses;
using System.Threading;
using System.Threading.Tasks;
using ProjectName.Application.Abstraction.Repositories;
using Mapster;
using ProjectName.Application.Abstraction.Messagings;
using MediatR;
using ProjectName.Application.Common.Handler;
using ProjectName.Application.Common.Responses;
using Microsoft.Extensions.Logging;

namespace ProjectName.Application.Features.Tenant.Command.Create;
public class CreateTenantHandler : GenericCreateHandler<Domain.Entities.Tenant.Tenant, ITenantRepository, TenantResponse,  CreateTenantCommand>
{
    public CreateTenantHandler(ILogger<CreateTenantHandler> logger, ITenantRepository repository, IUnitOfWork unitOfWork) 
        : base(logger, repository, unitOfWork)
    {
    }
}
