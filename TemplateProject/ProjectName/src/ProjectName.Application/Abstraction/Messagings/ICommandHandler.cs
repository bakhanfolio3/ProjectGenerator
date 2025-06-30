using ProjectName.Application.Abstraction.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Messagings;
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, IResult>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, IResult<TResponse>>
       where TCommand : ICommand<TResponse>
        where TResponse : class, IResponse
{
}
