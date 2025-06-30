using ProjectName.Domain.Entities.Common;
using ProjectName.Domain.Entities.Enums;
using ProjectName.Domain.Entities.Tenant;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectName.Domain.Entities.Identity;

public class User : TrackableEntity, IEntity, ITrackable, ISoftDeletable
{
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone1 { get; set; } = null!;
    public string? Phone2 { get; set; } = null!;
    public bool IsSuperUser { get; set; }    
    public bool IsDeleted { get; set; }

    public int UserRoleId { get; set; }
    public int? TenantId { get; set; } = null;

    [ForeignKey(nameof(UserRoleId))]
    public Role UserRole { get; set; } = null!;
    [ForeignKey(nameof(TenantId))]
    public Domain.Entities.Tenant.Tenant Tenant { get; set; } = null!;
    [InverseProperty(nameof(User.Authentication.User))]
    public Authentication Authentication { get; set; } = null!;
}
