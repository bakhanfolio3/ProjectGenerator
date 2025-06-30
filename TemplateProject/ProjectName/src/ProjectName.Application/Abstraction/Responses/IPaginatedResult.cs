using ProjectName.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Responses;
public interface IPaginatedResult<T> : IListResult<T> where T : class
{
    int PageNumber { get; }

    int PageSize { get; }

    int TotalPages { get; }

    int TotalCount { get; }
}
