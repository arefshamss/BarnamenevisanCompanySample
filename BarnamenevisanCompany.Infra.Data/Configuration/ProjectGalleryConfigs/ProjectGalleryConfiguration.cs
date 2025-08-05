using BarnamenevisanCompany.Domain.Models.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.ProjectGalleryConfigs;

public class ProjectGalleryConfiguration : IEntityTypeConfiguration<ProjectGallery>
{
    public void Configure(EntityTypeBuilder<ProjectGallery> builder)
    {
        #region Key

        builder.HasKey(s => s.Id);

        #endregion

        #region Property

        builder.Property(s => s.ImageName).HasMaxLength(250).IsRequired();

        #endregion

        #region Relation

        builder.HasOne(u => u.Project)
            .WithMany(u => u.ProjectGalleries)
            .HasForeignKey(u => u.ProjectId);

        #endregion
    }
}