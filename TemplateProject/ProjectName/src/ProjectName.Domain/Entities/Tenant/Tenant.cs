using ProjectName.Domain.Entities.Common;
using ProjectName.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Domain.Entities.Tenant;
public class Tenant : TrackableEntity, IEntity, ITrackable, ISoftDeletable, INameValueEntity<int>
{
    public required string Name { get; set; }
    public string? RefName { get; set; }
    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
    public required string Address1 { get; set; }
    public string? Address2 { get; set; }
    public required string Email { get; set; }
    public string? Logo { get; set; }

    public int IsActive { get; set; }
    public bool IsDeleted { get; set; }



    [InverseProperty(nameof(Identity.User.Tenant))]
    public ICollection<User> Users { get; set; } = new List<User>();

    public int Value => Id;
}
