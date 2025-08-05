using BarnamenevisanCompany.Domain.Models.AboutUs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.AboutUsConfigs;

public class AboutUsConfig : IEntityTypeConfiguration<AboutUs>
{
    public void Configure(EntityTypeBuilder<AboutUs> builder)
    {
        #region Key

        builder.Property(x => x.Id);

        #endregion

        #region Property
        builder.Property(x => x.MainDescriptionLeft)
            .IsRequired()
            .HasMaxLength(600);

        builder.Property(x => x.MainDescriptionRight)
            .HasMaxLength(600);
        
        builder.Property(x => x.OurValuesTitle)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.OurPassionDescription)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.TransparencyDescription)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.OurMissionDescription)
            .IsRequired()
            .HasMaxLength(1000);
        
       builder.Property(s=>s.TopDescription)
           .IsRequired()
           .HasMaxLength(1000);
       
       builder.Property(s=>s.TopTitle)
           .IsRequired()
           .HasMaxLength(200);
       builder.Property(s=>s.OurValuesDescription)
           .IsRequired()
           .HasMaxLength(1000);

        #endregion
    }
}