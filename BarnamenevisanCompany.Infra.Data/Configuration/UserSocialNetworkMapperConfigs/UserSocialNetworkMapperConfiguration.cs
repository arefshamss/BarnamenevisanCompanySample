using BarnamenevisanCompany.Domain.Models.UserSocialNetworkMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.UserSocialNetworkMapperConfigs;

public class UserSocialNetworkMapperConfiguration : IEntityTypeConfiguration<UserSocialNetworkMapping>
{
    public void Configure(EntityTypeBuilder<UserSocialNetworkMapping> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Property

        builder.Property(x => x.SocialLink).IsRequired().HasMaxLength(400);

        #endregion

        #region Relation

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserSocialNetworkMappings)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.UserSocialNetwork)
            .WithMany(x => x.UserSocialNetworkMapper)
            .HasForeignKey(x => x.SocialNetworkId);

        #endregion
    }
}