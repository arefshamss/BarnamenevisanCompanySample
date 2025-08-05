using BarnamenevisanCompany.Domain.Models.AboutUs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.AboutUsConfigs;

    public class AboutUsCommentConfig : IEntityTypeConfiguration<AboutUsComment>
    {
        public void Configure(EntityTypeBuilder<AboutUsComment> builder)
        {
            #region Key

            builder.Property(x => x.Id);

            #endregion

            #region Property

            builder.Property(x => x.Comment)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Rating)
                .IsRequired();

            #endregion
            
            #region Relations

            builder.HasOne(u => u.Users)
                .WithMany(u => u.AboutUsComments)
                .HasForeignKey(u => u.UserId);

            #endregion
        }
    }