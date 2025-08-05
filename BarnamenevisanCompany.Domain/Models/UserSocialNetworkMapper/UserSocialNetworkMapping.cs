using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.UserSocialNetworkMapper;

public class UserSocialNetworkMapping:BaseEntity<short>
{
    #region Property
    
    public int UserId { get; set; }

    public byte SocialNetworkId { get; set; }

    public required string SocialLink { get; set; }

    #endregion

    #region Relation

    public User.User User { get; set; }

    public UserSocialNetwork.UserSocialNetwork UserSocialNetwork { get; set; }

    #endregion
}