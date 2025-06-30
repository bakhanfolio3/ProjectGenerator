using ProjectName.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Domain.Entities.Identity;

//[UserID], [LoginID], [UserPasswordHash], [PasswordExpiryDate], [LastLoginTime], [WrongRetriesRemaining], [isLocked], [UserStatus], [UpdatedOn], [UpdatedBy]
public class Authentication : TrackableEntity, IEntity, ITrackable
{
    public string Email { get; set; } = null!;
    public required string PasswordHash { get; set; }
    public DateTime PasswordExpiryDate { get; set; }
    public DateTime LastLoginTime { get; set; }
    public int WrongRetriesRemaining { get; set; }
    public bool isLocked { get; set; }
    public bool IsActive { get; set; }
    public bool EmailConfirmed { get; set; }

    public int UserID { get; set; }

    [ForeignKey(nameof(UserID))]
    public User User { get; set; } = null!;
}
