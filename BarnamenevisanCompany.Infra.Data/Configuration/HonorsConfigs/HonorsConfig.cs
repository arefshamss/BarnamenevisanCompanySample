using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.HonorsConfigs;

public class HonorsConfig : IEntityTypeConfiguration<Domain.Models.Honors.Honors>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Honors.Honors> builder)
    {
        #region Key

        builder.HasKey(u => u.Id);

        #endregion

        #region Property

        builder.Property(u => u.Title).HasMaxLength(250).IsRequired();
        builder.Property(u => u.Description).HasMaxLength(1500);
        builder.Property(u => u.ImageName).HasMaxLength(250).IsRequired();
        builder.Property(u => u.TeaserName).HasMaxLength(250);
        builder.Property(u => u.Slug).HasMaxLength(250).IsRequired();
        builder.Property(u => u.TeaserPoster).HasMaxLength(250);

        #endregion

        #region Relation

        builder.HasOne(u => u.Company)
            .WithMany(u => u.Honors)
            .HasForeignKey(u => u.CompanyId);

        builder.HasMany(u => u.HonorsGalleries)
            .WithOne(u => u.Honors)
            .HasForeignKey(u => u.HonorsId);

        #endregion
    }
}