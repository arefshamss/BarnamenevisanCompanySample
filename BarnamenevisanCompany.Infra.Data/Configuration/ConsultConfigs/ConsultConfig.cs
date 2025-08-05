using BarnamenevisanCompany.Domain.Models.Consult;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.ConsultConfigs;

public class ConsultConfig : IEntityTypeConfiguration<Consult>
{
    public void Configure(EntityTypeBuilder<Consult> builder)
    {
        #region Key

        builder.HasKey(c => c.Id);

        #endregion

        #region Property

        builder.Property(c => c.Title).IsRequired().HasMaxLength(250);
        builder.Property(c => c.Description).IsRequired().HasMaxLength(1500);
        builder.Property(c => c.AdminMessage).HasMaxLength(1500).IsRequired(false);
        builder.Property(c => c.FirstName).HasMaxLength(60).IsRequired();
        builder.Property(c => c.LastName).HasMaxLength(60).IsRequired();
        builder.Property(c => c.Mobile).HasMaxLength(11).IsRequired();

        #endregion

        #region Relation

        builder.HasOne(u => u.User)
            .WithMany(u => u.Consults)
            .HasForeignKey(u => u.UserId)
            .IsRequired(false);

        #endregion
    }
}