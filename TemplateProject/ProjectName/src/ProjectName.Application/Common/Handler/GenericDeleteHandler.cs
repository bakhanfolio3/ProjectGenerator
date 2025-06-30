using ProjectName.Application.Abstraction.Repositories;
using ProjectName.Application.Abstraction.Responses;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using ProjectName.Application.Abstraction.Messagings;
using ProjectName.Domain.Entities.Common;
using ProjectName.Application.Common.Responses;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ProjectName.Application.Common.ThrowR;
using Microsoft.Extensions.Logging;

namespace ProjectName.Application.Common.Handler;
public abstract class GenericDeleteHandler<TEntity, TRepository, TCommand> : ICommandHandler<TCommand>
    where TEntity : class, IEntity
    where TCommand : class, IDeleteCommand
    where TRepository : IGenericRepository<TEntity>
{
    public GenericDeleteHandler(ILogger logger, TRepository repository, IUnitOfWork unitOfWork)
    {
        Logger = logger;
        Repository = repository;
        UnitOfWork = unitOfWork;
    }

    public ILogger Logger { get; }
    public TRepository Repository { get; }
    public IUnitOfWork UnitOfWork { get; }

    public async Task<IResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        TEntity? existingEntity = await Repository.GetByIdAsync(command.Id);
        Throw.Exception.IfNull(existingEntity, "Entity not found");
#pragma warning disable CS8604 // Possible null reference argument.
        await Repository.DeleteAsync(existingEntity, cancellationToken);
#pragma warning restore CS8604 // Possible null reference argument.
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
