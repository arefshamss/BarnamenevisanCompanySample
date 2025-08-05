using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.HonorsGalleryConfigs;

public class HonorsGalleryConfig:IEntityTypeConfiguration<Domain.Models.Honors.HonorsGallery>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Honors.HonorsGallery> builder)
    {
        #region Key

        builder.HasKey(p => p.Id);
        
        

        #endregion

        #region Property

        builder.Property(p => p.ImageName).HasMaxLength(300).IsRequired();

        #endregion

        #region Relation

        builder.HasOne(u => u.Honors)
            .WithMany(u => u.HonorsGalleries)
            .HasForeignKey(u => u.HonorsId);

        #endregion
    }
}