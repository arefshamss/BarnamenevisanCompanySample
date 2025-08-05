using BarnamenevisanCompany.Domain.Models.SiteSetting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.SiteSettingConfigs;

public class SiteSettingConfig:IEntityTypeConfiguration<SiteSetting>
{
    public void Configure(EntityTypeBuilder<SiteSetting> builder)
    {
        #region Key

        builder.HasKey(u=> u.Id);

        #endregion

        #region Properties  

        builder.Property(u => u.ConsultInformationText).HasMaxLength(1500).IsRequired();
        builder.Property(u => u.ContactUsHeaderText).HasMaxLength(1500).IsRequired();
        builder.Property(u => u.SiteLogo).HasMaxLength(200).IsRequired();
        builder.Property(u => u.CompletedProject).IsRequired();
        builder.Property(u => u.ContactUsFooter).HasMaxLength(1500).IsRequired();
        builder.Property(u => u.IndexOrderTitle).HasMaxLength(300).IsRequired();
        builder.Property(u => u.IndexParagraph).HasMaxLength(1500).IsRequired();
        builder.Property(u => u.IndexTitle).HasMaxLength(300).IsRequired();
        builder.Property(u => u.IndexOrderParaghraph).HasMaxLength(1500).IsRequired();
        builder.Property(u => u.AboutUsFooter).HasMaxLength(500).IsRequired();
        builder.Property(u => u.FavIcon).HasMaxLength(200).IsRequired();
        builder.Property(u => u.JobListTitle).HasMaxLength(100).IsRequired();
        builder.Property(u => u.JobListDescription).HasMaxLength(1500).IsRequired();
        builder.Property(u => u.OurServicePageTitle).HasMaxLength(100).IsRequired();
        builder.Property(u => u.OurServiceDescription).HasMaxLength(1500).IsRequired();
        builder.Property(u => u.ConsultSmsMessage).HasMaxLength(500).IsRequired();
        builder.Property(u => u.NotificationPhoneNumber).HasMaxLength(11).IsRequired(false);
        
        #endregion
    }
}