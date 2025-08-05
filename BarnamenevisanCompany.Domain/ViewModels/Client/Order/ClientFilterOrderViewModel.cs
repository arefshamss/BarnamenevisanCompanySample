using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Order;

public class ClientFilterOrderViewModel : BasePaging<ClientOrderViewModel>
{
    public int UserId { get; set; }
}