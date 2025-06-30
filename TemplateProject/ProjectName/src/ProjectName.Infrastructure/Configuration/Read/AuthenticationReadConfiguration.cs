using ProjectName.Domain.Entities.Common;
using ProjectName.Domain.Entities.Identity;
using ProjectName.Domain.Entities.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectName.Infrastructure.Configuration.Read;

public class AuthenticationReadConfiguration : IEntityTypeConfiguration<Authentication>
{
    public void Configure(EntityTypeBuilder<Authentication> builder)
    {
        builder.HasKey(x => x.Id);
    }
}