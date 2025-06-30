using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Requests;
public interface ISearchRequest : IRequest
{
    string? SearchText { get; set; }
}
