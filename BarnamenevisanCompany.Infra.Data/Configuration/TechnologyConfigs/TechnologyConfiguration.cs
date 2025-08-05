using BarnamenevisanCompany.Domain.Models.Technology;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.TechnologyConfigs;

public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
{
    public void Configure(EntityTypeBuilder<Technology> builder)
    {
        #region Key

        builder.HasKey(s => s.Id);

        #endregion

        #region Property

        builder.Property(s => s.Title).HasMaxLength(250).IsRequired();
        builder.Property(s => s.IconName).HasMaxLength(250).IsRequired();

        #endregion
        
    }
}