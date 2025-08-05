using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.CompanyConfigs;

public class CompanyConfig : IEntityTypeConfiguration<Domain.Models.Company.Company>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Company.Company> builder)
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
        
        builder.Property(x => x.Description)
            .HasMaxLength(700)
            .IsRequired(false);

        #endregion

        #region Relations

        #endregion
    }
}