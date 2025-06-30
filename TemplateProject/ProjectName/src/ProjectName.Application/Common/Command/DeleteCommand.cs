using ProjectName.Application.Abstraction.Messagings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Common.Command;
public class DeleteCommand: IDeleteCommand
{
    public int Id { get; set; }

}

