using BarnamenevisanCompany.Domain.Models.ContactUs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.ContactUsConfigs;

public class ContactUsConfig:IEntityTypeConfiguration<ContactUs>
{
    public void Configure(EntityTypeBuilder<ContactUs> builder)
    {
        #region Key

        builder.HasKey(u => u.Id);

        #endregion

        #region Property

        builder.Property(u=> u.Title).HasMaxLength(200).IsRequired();
        builder.Property(u=> u.Email).HasMaxLength(200).IsRequired();
        builder.Property(u=> u.PhoneNumber).HasMaxLength(11).IsRequired();
        builder.Property(u=>u.FullName).HasMaxLength(200).IsRequired();
        builder.Property(u=> u.Message).HasMaxLength(2500).IsRequired();
        builder.Property(u=> u.AdminMessage).HasMaxLength(500);
        

        #endregion
    }
}