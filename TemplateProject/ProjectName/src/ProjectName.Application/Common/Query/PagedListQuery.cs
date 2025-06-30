using ProjectName.Application.Abstraction.Messagings;
using ProjectName.Application.Abstraction.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Common.Query;
public class PagedListQuery<TResponse> : IPagedListQuery<TResponse>
    where TResponse : class, IResponse
{
    public string? SearchText { get; set; }
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 20;
}
