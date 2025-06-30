using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.CacheKeys;
public static class TenantCacheKeys
{
    public static string ListKey => "TenantList";

    public static string SelectListKey => "TenantSelectList";
    public static string ListCountKey => "TenantListCount";

    public static string GetListKey(string? queryString) => $"TenantList-{queryString}";
    public static string GetNVListKey(string? queryString) => $"TenantNVList-{queryString}";

    public static string GetKey(int TenantId) => $"Tenant-{TenantId}";

    public static string GetDetailsKey(int TenantId) => $"BrandDetails-{TenantId}";
}


