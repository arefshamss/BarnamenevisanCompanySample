using BarnamenevisanCompany.Domain.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.OrderConfigs;

public class OrderTypeMappingConfig : IEntityTypeConfiguration<OrderTypeMapping>
{
    public void Configure(EntityTypeBuilder<OrderTypeMapping> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Validations

        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderTypeMapping)
            .HasForeignKey(x => x.OrderId);
        
        builder.HasOne(x => x.OrderType)
            .WithMany(x => x.OrderTypeMapping)
            .HasForeignKey(x => x.OrderTypeId);

        #endregion
    }
}