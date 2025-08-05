using BarnamenevisanCompany.Domain.Models.Common;
using BarnamenevisanCompany.Domain.Models.Project;

namespace BarnamenevisanCompany.Domain.Models.UserPosition;

public sealed class UserPosition : BaseEntity<short>
{
    #region Property

    public int UserId { get; set; }

    public required string Position { get; set; }

    public string? WebsiteAddress { get; set; }

    public short Priority { get; set; }

    #endregion

    #region Relations

    public User.User Users { get; set; }

    public ICollection<ProjectMemberMapping> ProjectMemberMappings { get; set; }


    #endregion
}

