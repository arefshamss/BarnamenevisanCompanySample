using BarnamenevisanCompany.Domain.Models.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.BlogConfigs;

public class BlogUserMappingConfig : IEntityTypeConfiguration<BlogUserMapping>
{
    public void Configure(EntityTypeBuilder<BlogUserMapping> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Validations

        builder.Property(x => x.UserIp)
            .IsRequired();

        #endregion

        #region Relations

        builder.HasOne(x => x.Blog)
            .WithMany(x => x.BlogUserMappings)
            .HasForeignKey(x => x.BlogId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.BlogUserMappings)
            .HasForeignKey(x => x.UserId);
        
        #endregion
    }
}