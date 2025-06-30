using ProjectName.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.CacheKeys;
public static class UserCacheKeys
{
    public static string ListKey => "UserList";

    public static string SelectListKey => "UserSelectList";

    public static string GetKey(int UserId) => $"User-{UserId}";

    public static string GetDetailsKey(int UserId) => $"UserDetails-{UserId}";
}
