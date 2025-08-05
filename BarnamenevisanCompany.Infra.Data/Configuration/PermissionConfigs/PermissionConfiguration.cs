using BarnamenevisanCompany.Domain.Models.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.PermissionConfigs;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Properties

        builder.Property(x => x.DisplayName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.UniqueName)
            .HasMaxLength(100)
            .IsRequired();
        
        #endregion
    }
}