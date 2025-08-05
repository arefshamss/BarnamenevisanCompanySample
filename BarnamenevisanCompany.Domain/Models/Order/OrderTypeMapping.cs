using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Order;

public sealed class OrderTypeMapping : BaseEntity
{
    #region Properties

    public int OrderId { get; set; }

    public short OrderTypeId { get; set; }

    #endregion

    #region Relations

    public Order Order { get; set; }

    public OrderType OrderType { get; set; }

    #endregion
}