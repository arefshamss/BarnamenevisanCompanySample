using BarnamenevisanCompany.Domain.Models.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.ProjectTechnologyMappingConfigs;

public class ProjectTechnologyMappingConfiguration:IEntityTypeConfiguration<ProjectTechnologyMapping>
{
    public void Configure(EntityTypeBuilder<ProjectTechnologyMapping> builder)
    {
        #region Key

        builder.HasKey(s => s.Id);

        #endregion

        #region Relation

        builder.HasOne(s => s.Technology)
            .WithMany(s => s.ProjectTechnologyMappings)
            .HasForeignKey(s => s.TechnologyId);
        
        builder.HasOne(s => s.Project)
            .WithMany(s => s.ProjectTechnologyMappings)
            .HasForeignKey(s => s.ProjectId);
        #endregion
    }
}