using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.UserSocialNetwork;

public class ClientFilterUserSocialNetworkListViewModel : BasePaging<ClientUserSocialNetworkListViewModel>
{
    public int UserId { get; set; }
}
