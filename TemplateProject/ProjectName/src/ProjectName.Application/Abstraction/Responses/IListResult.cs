using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Responses;

public interface IListResult<T> : IResult<List<T>> where T : class
{

}

