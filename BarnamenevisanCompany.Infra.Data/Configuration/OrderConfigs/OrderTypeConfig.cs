using BarnamenevisanCompany.Domain.Enums.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderType = BarnamenevisanCompany.Domain.Models.Order.OrderType;

namespace BarnamenevisanCompany.Infra.Data.Configuration.OrderConfigs;

public class OrderTypeConfig : IEntityTypeConfiguration<OrderType>
{
    public void Configure(EntityTypeBuilder<OrderType> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Validations

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.ImageUrl)
            .IsRequired(false);
        
        #endregion
    }
}