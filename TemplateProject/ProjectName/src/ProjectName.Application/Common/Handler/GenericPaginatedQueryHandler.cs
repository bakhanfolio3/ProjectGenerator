using ProjectName.Application.Abstraction.Messagings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectName.Application.Abstraction.Responses;
using System.Threading;
using ProjectName.Application.Common.Responses;
using ProjectName.Application.Abstraction.CacheRepositories;
using ProjectName.Domain.Entities.Common;
using System.Linq.Expressions;
using Mapster;
using System.Web;
using ProjectName.Application.Common.Query;
using Microsoft.Extensions.Logging;

namespace ProjectName.Application.Common.Handler;
public abstract class GenericPaginatedQueryHandler<TEntity, TCacheRepository, TReponse, TQuery> : IPaginatedListQueryHandler<TQuery, TReponse>
    where TReponse : class, IResponse
    where TEntity : class, IEntity
    where TQuery : class, IPagedListQuery<TReponse>
    where TCacheRepository : IGenericCacheRepository<TEntity>
{
    public GenericPaginatedQueryHandler(ILogger logger, TCacheRepository cacheRepository)
    {
        Logger = logger;
        CacheRepository = cacheRepository;
    }

    public bool DisableCache { get; set; }
    public ILogger Logger { get; }
    public TCacheRepository CacheRepository { get; }

    public virtual async Task<IPaginatedResult<TReponse>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var response = await CacheRepository.GetCachedListAsync(request.PageNumber, request.PageSize,
            GetListPredicate(request), GetQueryString(request), DisableCache,
            cancellationToken);
        return PaginatedResult<TReponse>.Success(response.Data.Adapt<List<TReponse>>(), response.TotalCount, response.PageNumber, response.PageSize);
    }

    protected abstract Expression<Func<TEntity, bool>> GetListPredicate(TQuery request);

    //generate the predicate for the request object of type TQuery though reflection
    //public virtual Expression<Func<TEntity, bool>> CreateListPredicate(TQuery request)
    //{
        
    //    var parameter = Expression.Parameter(typeof(TEntity), "x");
    //    Expression expression = Expression.Constant(true);
    //    var properties = typeof(TQuery).GetProperties();
    //    foreach (var property in properties)
    //    {
    //        var propertyValue = property.GetValue(request);
    //        if (propertyValue == null)
    //        {
    //            continue;
    //        }
    //        var propertyType = property.PropertyType;
    //        var propertyTypeCode = Type.GetTypeCode(propertyType);
    //        var propertyName = property.Name;
    //        var memberExpression = Expression.Property(parameter, propertyName);
    //        var constantExpression = Expression.Constant(propertyValue);
    //        Expression propertyExpression = memberExpression;
    //        switch (propertyTypeCode)
    //        {
    //            case TypeCode.String:
    //                propertyExpression = Expression.Call(memberExpression, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constantExpression);
    //                break;
    //            case TypeCode.Int32:
    //                propertyExpression = Expression.Equal(memberExpression, constantExpression);
    //                break;
    //        }
    //        expression = Expression.AndAlso(expression, propertyExpression);
    //    }
    //    return Expression.Lambda<Func<TEntity, bool>>(expression, parameter);
    //}

    // generate the query string for the request object of type TQuery 
    public virtual string GetQueryString(TQuery request)
    {
        var properties = typeof(PagedListQuery<TReponse>).GetProperties();
        var queryString = new StringBuilder();
        foreach (var property in properties)
        {
            var propertyValue = property.GetValue(request);
            if (propertyValue == null)
            {
                continue;
            }
            var propertyName = property.Name;
            queryString.Append($"{propertyName}={HttpUtility.UrlEncode(propertyValue.ToString())}&");
        }
        return queryString.ToString();
    }

}
