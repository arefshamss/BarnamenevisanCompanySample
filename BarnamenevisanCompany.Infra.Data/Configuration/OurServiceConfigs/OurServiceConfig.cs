using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.OurServiceConfigs;

public class OurServiceConfig:IEntityTypeConfiguration<Domain.Models.OurServices.OurService>
{
    public void Configure(EntityTypeBuilder<Domain.Models.OurServices.OurService> builder)
    {
        #region Key

        builder.HasKey(u => u.Id);

        #endregion

        #region Properties  

        builder.Property(u => u.Title).IsRequired().HasMaxLength(150);
        builder.Property(u => u.ImageName).IsRequired().HasMaxLength(300);
        builder.Property(u => u.ShortDescription).IsRequired().HasMaxLength(500);
        builder.Property(u => u.LongDescription).IsRequired();
        builder.Property(u=> u.Slug).IsRequired().HasMaxLength(250);

        #endregion
    }
}