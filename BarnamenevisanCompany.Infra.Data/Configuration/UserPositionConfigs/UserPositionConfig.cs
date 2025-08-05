using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.UserPositionConfigs;

public class UserPositionConfig:IEntityTypeConfiguration<Domain.Models.UserPosition.UserPosition>
{
    public void Configure(EntityTypeBuilder<Domain.Models.UserPosition.UserPosition> builder)
    {
        #region key

        builder.HasKey(p => p.Id);

        #endregion

        #region Property

        builder.Property(p => p.WebsiteAddress).HasMaxLength(150);
        builder.Property(p => p.Position).HasMaxLength(150).IsRequired();
        builder.Property(p => p.Priority).IsRequired();


        #endregion

        #region Relation

        builder.HasOne(u => u.Users)
            .WithMany(u => u.UserPositions)
            .HasForeignKey(u => u.UserId);

        #endregion
    }
}