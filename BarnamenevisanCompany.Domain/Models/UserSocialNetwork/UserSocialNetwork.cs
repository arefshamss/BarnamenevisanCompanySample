using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.UserSocialNetwork;

public sealed class UserSocialNetwork:BaseEntity<byte>
{
    #region Property

    public required string Title { get; set; }

    public required string PersianTitle { get; set; }

    public required string ImageName { get; set; }
    

    #endregion

    #region Relation

    public ICollection<UserSocialNetworkMapper.UserSocialNetworkMapping> UserSocialNetworkMapper { get; set; }

    #endregion
}