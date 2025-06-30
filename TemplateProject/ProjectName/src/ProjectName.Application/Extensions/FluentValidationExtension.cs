using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Extensions;
public static class FluentValidationExtension
{
    public static List<string> AsErrors(this FluentValidation.Results.ValidationResult result)
    {
        return result.Errors.Select(e => e.ErrorMessage).ToList();
    }
}
