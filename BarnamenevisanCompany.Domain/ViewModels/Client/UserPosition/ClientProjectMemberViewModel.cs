using BarnamenevisanCompany.Domain.ViewModels.Client.UserSocialNetwork;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.UserPosition;

public class ClientProjectMemberViewModel
{
    public string FullName { get; set; }

    public string UserImage { get; set; }

    public short Priority { get; set; }

    public string SiteUrl { get; set; }

    public string UserPosition { get; set; }

    public List<ClientUserSocialNetworkViewModel> UserSocialNetworkViewModels { get; set; }
}