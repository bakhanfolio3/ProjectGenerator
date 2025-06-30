using ProjectName.Application.Abstraction.ThrowR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Common.ThrowR;
public class Throw : IThrow
{
    public static IThrow Exception { get; } = new Throw();


    private Throw()
    {
    }
}
