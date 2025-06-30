using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Domain.Entities.Common;
public interface INameValueEntity<T>
{
    public string Name { get; }
    public T Value { get; }
}
