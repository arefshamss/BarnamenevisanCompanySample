using BarnamenevisanCompany.Domain.Models.Job;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.JobConfigs;

public class jobConfig : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Validations

        builder.Property(x => x.Title)
            .HasMaxLength(300)
            .IsRequired();      
        
        builder.Property(x => x.Slug)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.ShortDescription)
            .HasMaxLength(700)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.WorkExperience)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.SalaryFrom)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.SalaryTo)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.ExpireDate)
            .IsRequired();

        builder.Property(x => x.Address)
            .HasMaxLength(1000)
            .IsRequired();

        #endregion
    }
}