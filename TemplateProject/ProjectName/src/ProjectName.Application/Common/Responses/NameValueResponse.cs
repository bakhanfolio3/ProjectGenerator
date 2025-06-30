using ProjectName.Application.Abstraction.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Common.Responses;
public class NameValueResponse<T> : INameValueResponse<T>
{
    public required T Value { get; set; }
    public required string Name { get; set; }
}
