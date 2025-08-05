using BarnamenevisanCompany.Domain.Models.UserSocialNetwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.UserSocialNetworkConfigs;

public class UserSocialNetworkConfiguration : IEntityTypeConfiguration<UserSocialNetwork>
{
    public void Configure(EntityTypeBuilder<UserSocialNetwork> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Property

        builder.Property(x => x.ImageName).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PersianTitle).IsRequired().HasMaxLength(100);

        #endregion
    }
}