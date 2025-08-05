using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Order;

public sealed class Order : BaseEntity
{
    #region Properties
    public required string Title { get; set; }

    public required string Description { get; set; }

    public string? Attachment { get; set; }

    public OrderStatus OrderStatus { get; set; }
    
    public string OrderNumber { get; set; }
    
    public string? Answer {get; set;}

    public int UserId { get; set; }

    #endregion

    #region Relations

    public User.User User { get; set; }
    
    public ICollection<OrderTypeMapping> OrderTypeMapping { get; set; }

    #endregion
}