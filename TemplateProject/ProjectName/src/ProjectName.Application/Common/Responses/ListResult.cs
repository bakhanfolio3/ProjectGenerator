using ProjectName.Application.Abstraction.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Common.Responses;
public class ListResult<T> : Result<List<T>>, IListResult<T> where T : class
{
    public new static IListResult<T> Success(List<T> data)
    {
        return new ListResult<T>
        {
            Succeeded = true,
            Data = data
        };
    }
}
