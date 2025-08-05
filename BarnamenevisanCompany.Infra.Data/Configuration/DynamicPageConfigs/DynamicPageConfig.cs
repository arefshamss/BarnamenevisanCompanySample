using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.DynamicPageConfigs;

public class DynamicPageConfig : IEntityTypeConfiguration<Domain.Models.DynamicPage.DynamicPage>
{
    public void Configure(EntityTypeBuilder<Domain.Models.DynamicPage.DynamicPage> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Validations

        builder.Property(x=>x.Title)
            .HasMaxLength(300)
            .IsRequired();
        
        builder.Property(x=>x.Slug)
            .HasMaxLength(400)
            .IsRequired();
        
        builder.Property(x=>x.ShortDescription)
            .HasMaxLength(1000)
            .IsRequired(false);
        
        builder.Property(x=>x.Description)
            .IsRequired(false);

        #endregion

        #region Relations



        #endregion
    }
}