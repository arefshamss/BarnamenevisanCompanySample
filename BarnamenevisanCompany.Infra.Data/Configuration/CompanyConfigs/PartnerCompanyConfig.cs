using BarnamenevisanCompany.Domain.Models.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.CompanyConfigs;

public class PartnerCompanyConfig : IEntityTypeConfiguration<PartnerCompany>
{
    public void Configure(EntityTypeBuilder<PartnerCompany> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Validations

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.SiteUrl)
            .HasMaxLength(200)
            .IsRequired(false);
        
        builder.Property(x => x.ImageUrl)
            .HasMaxLength(200)
            .IsRequired(false);
        
        builder.Property(x => x.ShortDescription)
            .HasMaxLength(500)
            .IsRequired(false);

        #endregion

        #region Relations

        #endregion
    }
}