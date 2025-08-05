using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Project;

public sealed class ProjectVisitsMapping : BaseEntity<int>
{
    #region Peoperties

    public int? UserId { get; set; }

    public required string UserIp { get; set; }

    public short ProjectId { get; set; }

    #endregion

    #region Relations

    public User.User? User { get; set; }

    public Project Project { get; set; }

    #endregion
}