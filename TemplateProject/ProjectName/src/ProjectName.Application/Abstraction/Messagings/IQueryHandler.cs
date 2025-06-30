using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Messagings;
public interface IQueryHandler<TQuery, TResponse>: MediatR.IRequestHandler<TQuery, Responses.IResult<TResponse>>
    where TQuery : class, IQuery<TResponse>
    where TResponse : class, Responses.IResponse
{
}

public interface IListQueryHandler<TQuery, TResponse> : MediatR.IRequestHandler<TQuery, Responses.IListResult<TResponse>>
    where TQuery : class, IListQuery<TResponse>
    where TResponse : class, Responses.IResponse
{
}

public interface IPaginatedListQueryHandler<TQuery, TResponse> : MediatR.IRequestHandler<TQuery, Responses.IPaginatedResult<TResponse>>
    where TQuery : class, IPagedListQuery<TResponse>
    where TResponse : class, Responses.IResponse
{
}