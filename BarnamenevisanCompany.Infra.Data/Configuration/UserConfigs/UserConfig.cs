using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.UserConfigs
{
    public class UserConfig : IEntityTypeConfiguration<Domain.Models.User.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.User.User> builder)
        {
            #region Key

            builder.HasKey(x => x.Id);

            #endregion

            #region Validations

            builder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.Password)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.Mobile)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();
            
            builder.Property(x => x.ActiveCode)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.ActiveCodeExpireTime)
                .IsRequired(false);

            builder.Property(x => x.AvatarImageName)
                .HasMaxLength(150)
                .IsRequired(false);

            #endregion
        }
    }
}
