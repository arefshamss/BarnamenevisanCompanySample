using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Project;

public sealed class ProjectMemberMapping:BaseEntity<short>
{
    #region Properties  

    public short UserId { get; set; }

    public short ProjectId { get; set; }

    public string UserPosition { get; set; }

    #endregion

    #region Relations

    public UserPosition.UserPosition User { get; set; }

    public Project Project { get; set; }

    #endregion
}