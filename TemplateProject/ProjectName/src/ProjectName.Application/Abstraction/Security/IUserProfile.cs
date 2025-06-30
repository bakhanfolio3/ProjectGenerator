using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Security;
public interface IUserProfile
{
    string Id { get; set; }
    string Username { get; set; }
    string? Role { get; set; }
}
