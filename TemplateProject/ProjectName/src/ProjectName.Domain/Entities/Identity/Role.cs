using ProjectName.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Domain.Entities.Identity;
public class Role : TrackableEntity, IEntity, ITrackable, ISoftDeletable
{
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    [InverseProperty(nameof(User.UserRole))]
    public ICollection<User> Users { get; set; } = new List<User>();
}
