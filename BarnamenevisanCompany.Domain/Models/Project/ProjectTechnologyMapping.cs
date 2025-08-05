using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Project;

public sealed class ProjectTechnologyMapping : BaseEntity<short>
{
    #region Properties

    public short TechnologyId { get; set; }

    public short ProjectId { get; set; }

    #endregion

    #region Relations

    public Technology.Technology Technology { get; set; }

    public Project Project { get; set; }

    #endregion
}