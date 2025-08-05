using BarnamenevisanCompany.Domain.Models.SocialNetwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.SocialNetworkConfigs;

public class SocialNetworkConfig:IEntityTypeConfiguration<SocialNetwork>
{
    public void Configure(EntityTypeBuilder<SocialNetwork> builder)
    {
        #region Key

        builder.HasKey(u => u.Id);

        #endregion

        #region Property

        builder.Property(u => u.Url).IsRequired().HasMaxLength(250);
        builder.Property(u => u.IconName).IsRequired().HasMaxLength(250);

        #endregion
    }
}