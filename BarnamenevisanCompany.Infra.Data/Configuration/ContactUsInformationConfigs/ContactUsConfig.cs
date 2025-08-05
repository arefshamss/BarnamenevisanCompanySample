using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.ContactUsInformationConfigs;

public class ContactUsInformationConfiguration:IEntityTypeConfiguration<Domain.Models.ContactUs.ContactUsInformation>
{
    public void Configure(EntityTypeBuilder<Domain.Models.ContactUs.ContactUsInformation> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Property

        builder.Property(x => x.Address).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Email).HasMaxLength(150);
        builder.Property(x=> x.PhoneNumber).HasMaxLength(150);
        builder.Property(x => x.Managment).HasMaxLength(300);
        
        #endregion
    }

  
}