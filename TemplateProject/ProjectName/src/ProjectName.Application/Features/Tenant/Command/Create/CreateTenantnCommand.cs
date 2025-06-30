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
using ProjectName.Application.Common.Responses;

namespace ProjectName.Application.Features.Tenant.Command.Create;
public class CreateTenantCommand : ICommand<TenantResponse>
{
    [MinLength(3)]
    public required string Name { get; set; }
    public string? RefName { get; set; }
    [Phone]
    public string? Phone1 { get; set; }
    [Phone]
    public string? Phone2 { get; set; }
    public required string Address1 { get; set; }
    public string? Address2 { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    [MaxLength(64)]
    public string? Logo { get; set; }
}