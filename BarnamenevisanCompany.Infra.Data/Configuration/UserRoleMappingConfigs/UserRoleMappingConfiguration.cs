using BarnamenevisanCompany.Domain.Models.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.UserRoleMappingConfigs;

public class UserRoleMappingConfiguration : IEntityTypeConfiguration<UserRoleMapping>
{
    public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Relations

        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.UserRoleMappings)
            .HasForeignKey(x => x.RoleId);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserRoleMappings)
            .HasForeignKey(x => x.UserId);
        
        #endregion
    }
}