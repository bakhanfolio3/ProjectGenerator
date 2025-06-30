using ProjectName.Domain.Entities.Common;
using ProjectName.Domain.Entities.Identity;
using ProjectName.Domain.Entities.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectName.Infrastructure.Configuration.Read;

public class TenantWriteConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasKey(x => x.Id);
    }
}