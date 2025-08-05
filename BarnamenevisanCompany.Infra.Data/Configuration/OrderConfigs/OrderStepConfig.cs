using BarnamenevisanCompany.Domain.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.OrderConfigs;

public class OrderStepConfig:IEntityTypeConfiguration<OrderStep>
{
    public void Configure(EntityTypeBuilder<OrderStep> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Property

        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.PriorityId).IsRequired();

        #endregion
    }
}