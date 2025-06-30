using ProjectName.Application.Abstraction.Messagings;
using ProjectName.Application.Abstraction.Requests;
using ProjectName.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Common.Query;
public class NVListQuery<TType> : IListQuery<NameValueResponse<TType>>, ISearchRequest
{
    public string? SearchText { get; set; }
}
