using BarnamenevisanCompany.Domain.Models.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.ProjectConfigs;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        #region Key

        builder.HasKey(s => s.Id);

        #endregion

        #region Property

        builder.Property(s => s.Title).HasMaxLength(250).IsRequired();
        builder.Property(s => s.ImageName).HasMaxLength(250).IsRequired();
        builder.Property(s => s.ProgressRate).IsRequired();
        builder.Property(s => s.TeaserName).HasMaxLength(250);
        builder.Property(s => s.Slug).IsRequired().HasMaxLength(250);
        builder.Property(s => s.SiteUrl).HasMaxLength(250).IsRequired();
        builder.Property(s => s.Description).HasMaxLength(3000).IsRequired();
        builder.Property(s => s.TeaserPoster).HasMaxLength(250);
        builder.Property(s => s.Priority).IsRequired();

        #endregion
    }
}