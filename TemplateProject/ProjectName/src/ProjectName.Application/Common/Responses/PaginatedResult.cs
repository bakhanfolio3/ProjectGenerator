using ProjectName.Application.Abstraction.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.Common.Responses;

public class PaginatedResult<T> : ListResult<T>, IPaginatedResult<T> where T : class
{
    public int PageNumber { get; init; }
    public int TotalPages { get; init; }

    public int PageSize { get; init; }

    public int TotalCount { get; init; }


    public PaginatedResult(List<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        PageSize = pageSize;
        if (Data == null)
            Data = items;
        else
            Data.AddRange(items);
    }

    public PaginatedResult()
    {

    }

    public static IPaginatedResult<T> Success(List<T> data, int count, int pageNumber, int pageSize)
    {
        return new PaginatedResult<T>
        {
            Succeeded = true,
            Data = data,
            PageNumber = pageNumber,
            TotalCount = count,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };
    }
}

public static class PaginatedListHelper
{
    public const int DefaultPageSize = 20;
    public const int DefaultCurrentPage = 1;

    public static async Task<IPaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source,
        int currentPage, int pageSize, CancellationToken cancellationToken = default)
        where T : class
    {
        currentPage = currentPage > 0 ? currentPage : DefaultCurrentPage;
        pageSize = pageSize > 0 ? pageSize : DefaultPageSize;
        var count = await source.CountAsync(cancellationToken);
        var items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return new PaginatedResult<T>(items, count, currentPage, pageSize);
    }
}