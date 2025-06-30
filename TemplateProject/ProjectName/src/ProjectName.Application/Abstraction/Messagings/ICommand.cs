using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Messagings;
public interface ICommand : IRequest<Responses.IResult>
{
}

public interface ICommand<TResponse> : IRequest<Responses.IResult<TResponse>>
    where TResponse : class, Responses.IResponse
{
}

public interface IUpdateCommand<TResponse> : ICommand<TResponse>
    where TResponse : class, Responses.IResponse
{
    int Id { get; set; }
}

public interface IDeleteCommand : ICommand
{
    int Id { get; set; }
}

