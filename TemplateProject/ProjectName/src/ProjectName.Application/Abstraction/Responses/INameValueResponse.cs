using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Responses;
public interface INameValueResponse<T> : IResponse
{
    public string Name { get; set; }
    public T Value { get; set; }
}
