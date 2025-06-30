using ProjectName.Application.Abstraction.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Services;
public interface IUserProfileService
{
    IUserProfile? GetLoggedInUserProfile();
}
