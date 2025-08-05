using BarnamenevisanCompany.Domain.ViewModels.Client.UserSocialNetwork;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.UserPosition;

public class ClientUserPositionViewModel
{
    public string UserName { get; set; }
    
    public string ImageName { get; set; }

    public string Position { get; set; }

    public string WebsiteAddress { get; set; }

    public short Priority { get; set; }

    public List<ClientUserSocialNetworkViewModel> UserSocialNetworkViewModels { get; set; }
    
}