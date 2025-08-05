using BarnamenevisanCompany.Domain.Models.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.RolePermissionMappingConfigs;

public class RolePermissionMappingConfiguration : IEntityTypeConfiguration<RolePermissionMapping>
{
    public void Configure(EntityTypeBuilder<RolePermissionMapping> builder)
    {
        #region Key

        builder.HasKey(x => new { x.PermissionId, x.RoleId });

        #endregion

        #region Relations

        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.RolePermissionMappings)
            .HasForeignKey(x => x.RoleId);

        builder
            .HasOne(x => x.Permission)
            .WithMany(x => x.RolePermissionMappings)
            .HasForeignKey(x => x.PermissionId);

        #endregion
    }
}