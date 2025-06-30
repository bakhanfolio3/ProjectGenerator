using ProjectName.Domain.Entities.Common;
using ProjectName.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectName.Infrastructure.Configuration.Write;

public class UserWriteConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        //builder.Property(x => x.Id)
        //    .HasConversion<HeroId.EfCoreValueConverter>();
    }
}