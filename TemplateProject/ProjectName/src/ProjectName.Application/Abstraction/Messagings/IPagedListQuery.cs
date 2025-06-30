using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Messagings;
public interface IPagedListQuery<TResponse> : IRequest<Responses.IPaginatedResult<TResponse>>
        where TResponse : class, Responses.IResponse
{
    string? SearchText { get; set; }
    int PageNumber { get; set; }
    int PageSize { get; set; }
}

