using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectName.Domain.Entities.Common;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}

