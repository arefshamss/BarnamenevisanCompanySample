using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.FaqConfigs;

public class FaqConfig : IEntityTypeConfiguration<Domain.Models.Faq.Faq>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Faq.Faq> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Validations

        builder.Property(x=>x.Question)
            .HasMaxLength(600)
            .IsRequired();
        
        builder.Property(x=>x.Answer)
            .HasMaxLength(700)
            .IsRequired();

        #endregion

        #region Relations

        

        #endregion
    }
}