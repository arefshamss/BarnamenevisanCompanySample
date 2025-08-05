using BarnamenevisanCompany.Domain.Models.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.ProjectMemberMappingConfigs;

public class ProjectMemberMappingConfiguration : IEntityTypeConfiguration<ProjectMemberMapping>
{
    public void Configure(EntityTypeBuilder<ProjectMemberMapping> builder)
    {
        #region Key

        builder.HasKey(s => s.Id);

        #endregion

        #region Property

        builder.Property(s => s.ProjectId).HasMaxLength(250).IsRequired();

        #endregion

        #region Relation

        builder.HasOne(s => s.Project)
            .WithMany(s => s.ProjectMemberMappings)
            .HasForeignKey(u => u.ProjectId);

        builder.HasOne(s => s.User)
            .WithMany(s => s.ProjectMemberMappings)
            .HasForeignKey(s => s.UserId);

        #endregion
    }
}