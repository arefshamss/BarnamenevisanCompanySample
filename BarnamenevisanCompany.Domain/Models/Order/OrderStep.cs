using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Order;

public class OrderStep:BaseEntity<short>
{
    #region Property

    public string Title { get; set; }

    public short PriorityId { get; set; }

    #endregion
}