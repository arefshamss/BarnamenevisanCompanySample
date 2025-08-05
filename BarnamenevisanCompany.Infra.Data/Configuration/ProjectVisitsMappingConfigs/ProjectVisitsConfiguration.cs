using BarnamenevisanCompany.Domain.Models.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.ProjectVisitsMappingConfigs;

public class ProjectVisitsConfiguration:IEntityTypeConfiguration<ProjectVisitsMapping>
{
    public void Configure(EntityTypeBuilder<ProjectVisitsMapping> builder)
    {
        #region Key

        builder.HasKey(x=>x.Id);

        #endregion

        #region Property

        builder.Property(x => x.UserIp).IsRequired().HasMaxLength(250);

        #endregion

        #region Relation

        builder.HasOne(x => x.User)
            .WithMany(x => x.ProjectVisitsMappings)
            .HasForeignKey(x => x.UserId);
        
        builder.HasOne(x => x.Project)
            .WithMany(x => x.ProjectVisitsMappings)
            .HasForeignKey(x => x.ProjectId);

        #endregion

    }
}