using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Order;

public sealed class OrderType : BaseEntity<short>
{
    #region Properties

    public required string Title { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    #endregion

    #region Relations

    public ICollection<OrderTypeMapping> OrderTypeMapping { get; set; }

    #endregion
}