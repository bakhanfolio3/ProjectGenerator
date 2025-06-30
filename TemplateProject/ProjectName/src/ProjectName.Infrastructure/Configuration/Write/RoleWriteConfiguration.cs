using ProjectName.Domain.Entities.Common;
using ProjectName.Domain.Entities.Identity;
using ProjectName.Domain.Entities.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectName.Infrastructure.Configuration.Read;

public class RoleWriteConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
    }
}