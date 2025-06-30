using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Abstraction.Responses;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using ProjectName.Application.Abstraction.Messagings;
using ProjectName.Domain.Entities.Common;
using ProjectName.Application.Common.Responses;
using Microsoft.Extensions.Logging;

namespace ProjectName.Application.Common.Handler;
public abstract class GenericCreateHandler<TEntity, TRepository, TResponse, TCommand> : ICommandHandler<TCommand, TResponse>
    where TEntity : class, IEntity
    where TResponse : class, IResponse
    where TCommand : class, ICommand<TResponse>
    where TRepository : IGenericRepository<TEntity>
{

    public GenericCreateHandler(ILogger logger, TRepository repository, IUnitOfWork unitOfWork)
    {
        Logger = logger;
        Repository = repository;
        UnitOfWork = unitOfWork;
    }

    public ILogger Logger { get; }
    public TRepository Repository { get; }
    public IUnitOfWork UnitOfWork { get; }

    public async Task<IResult<TResponse>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        Logger.LogDebug($"Create {typeof(TEntity).Name} with request: {request}");
        var created = request.Adapt<TEntity>();
        TEntity newEntity = await Repository.InsertAsync(created, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        TResponse response = newEntity.Adapt<TResponse>();
        return Result<TResponse>.Success(response);
    }
}
