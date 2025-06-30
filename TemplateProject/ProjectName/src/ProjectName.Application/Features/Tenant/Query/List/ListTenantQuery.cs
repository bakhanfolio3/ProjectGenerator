using ProjectName.Application.Features.Users;
using ProjectName.Application.Abstraction.Responses;
using ProjectName.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectName.Application.Abstraction.Messagings;
using ProjectName.Application.Common.Query;

namespace ProjectName.Application.Features.Tenant.Query.List;
public class ListTenantQuery : PagedListQuery<TenantResponse>
{
    public string? Name { get; set; }
    public string? RefName { get; set; }
    public string? Email { get; set; }
}

