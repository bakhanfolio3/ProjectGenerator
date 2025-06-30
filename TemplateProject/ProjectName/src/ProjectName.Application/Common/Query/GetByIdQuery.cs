using ProjectName.Application.Abstraction.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Common.Query;
public record GetByIdQuery<TResponse>(int Id) : IRequest<IResult<TResponse>>
    where TResponse : class, IResponse
{ }

