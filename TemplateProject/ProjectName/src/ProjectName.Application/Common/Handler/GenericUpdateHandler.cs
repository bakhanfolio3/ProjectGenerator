using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Abstraction.Responses;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using ProjectName.Application.Abstraction.Messagings;
using ProjectName.Domain.Entities.Common;
using ProjectName.Application.Common.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ProjectName.Application.Common.ThrowR;
using Microsoft.Extensions.Logging;

namespace ProjectName.Application.Common.Handler;
public abstract class GenericUpdateHandler<TEntity, TRepository, TResponse, TCommand> : ICommandHandler<TCommand, TResponse>
    where TEntity : class, IEntity
    where TResponse : class, IResponse
    where TCommand : class, IUpdateCommand<TResponse>
    where TRepository : IGenericRepository<TEntity>
{
    public GenericUpdateHandler(ILogger logger, TRepository repository, IUnitOfWork unitOfWork)
    {
        Logger = logger;
        Repository = repository;
        UnitOfWork = unitOfWork;
    }

    public ILogger Logger { get; }
    public TRepository Repository { get; }
    public IUnitOfWork UnitOfWork { get; }

    public async Task<IResult<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var existingEntity = await Repository.GetByIdAsync(command.Id);
        if (existingEntity == null)
            return Result<TResponse>.Fail("Entity not found");//NotFoundMessage<TEntity>.Get(command.Id)
        existingEntity = command.Adapt<TEntity>();
        await Repository.UpdateAsync(existingEntity);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result<TResponse>.Success(existingEntity.Adapt<TResponse>());
    }
}
