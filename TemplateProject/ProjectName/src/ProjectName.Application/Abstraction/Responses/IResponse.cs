using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Responses;
public interface IResponse
{

}

public interface IResponse<T> : IResponse
{
    public T Data { get; set; }
}
