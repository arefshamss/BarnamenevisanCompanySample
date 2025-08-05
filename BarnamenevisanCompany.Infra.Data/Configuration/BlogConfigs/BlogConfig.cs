using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.BlogConfigs;

public class BlogConfig : IEntityTypeConfiguration<Domain.Models.Blog.Blog>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Blog.Blog> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Validations

        builder.Property(x => x.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Slug)
            .HasMaxLength(350)
            .IsRequired();

        builder.Property(x => x.ShortDescription)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(300)
            .IsRequired();

        #endregion

        #region Relations

        builder.HasOne(x => x.User)
            .WithMany(x => x.Blogs)
            .HasForeignKey(x => x.UserId);

        #endregion
    }
}