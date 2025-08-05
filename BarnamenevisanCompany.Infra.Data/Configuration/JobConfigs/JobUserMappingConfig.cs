using BarnamenevisanCompany.Domain.Models.Job;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.JobConfigs;

public class JobUserMappingConfig : IEntityTypeConfiguration<JobUserMapping>
{
    public void Configure(EntityTypeBuilder<JobUserMapping> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Validations

        builder.Property(x => x.Description)
            .HasMaxLength(1200)
            .IsRequired(false);

        builder.Property(x => x.Attachment)
            .IsRequired();

        #endregion
        
        #region Relations

        builder.HasOne(x => x.Job)
            .WithMany(x => x.JobUserMappings)
            .HasForeignKey(x => x.JobId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.JobUserMappings)
            .HasForeignKey(x => x.UserId);
        
        #endregion
    }
}