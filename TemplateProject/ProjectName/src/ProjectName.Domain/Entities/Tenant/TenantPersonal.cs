using ProjectName.Domain.Entities.Common;
using ProjectName.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Domain.Entities.Tenant;
public class TenantPersonal : IEntity
{ 
    public int Id { get; set; }
    public int TenantId { get; set; }
    public int AdminUserId { get; set; }

    [ForeignKey(nameof(TenantId))]
    public Tenant? Tenant { get; set; }
    [ForeignKey(nameof(AdminUserId))]
    public User? Admin { get; set; }
}
