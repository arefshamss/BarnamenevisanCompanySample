using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.SocialNetwork;

public sealed class SocialNetwork:BaseEntity<byte>
{
    #region Properties

    public required string Title { get; set; }

    public required string IconName { get; set; }

    public required string Url { get; set; }

    #endregion
}