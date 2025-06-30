using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Messagings;
public interface IQuery<TResponse> : IRequest<Responses.IResult<TResponse>>
        where TResponse : class, Responses.IResponse
{
}

public interface IListQuery<TResponse> : IRequest<Responses.IListResult<TResponse>>
        where TResponse : class, Responses.IResponse
{
}

public interface INVListQuery<TResponse, T> : IRequest<Responses.IListResult<TResponse>>
        where TResponse : class, Responses.INameValueResponse<T>
{
}

