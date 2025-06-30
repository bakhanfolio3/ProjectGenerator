using ProjectName.Application.Abstraction.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Features.Tenant;
public class TenantResponse : IResponse
{
    public int Id { get; set; }
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

    public DateTime CreatedAt { get; set; }
    [StringLength(64)]
    public string? CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    [StringLength(64)]
    public string? UpdatedBy { get; set; }
}
