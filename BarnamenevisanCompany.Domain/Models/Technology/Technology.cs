using BarnamenevisanCompany.Domain.Models.Common;
using BarnamenevisanCompany.Domain.Models.Project;

namespace BarnamenevisanCompany.Domain.Models.Technology;

public sealed class Technology : BaseEntity<short>
{
    #region Property

    public required string Title { get; set; }

    public required string IconName { get; set; }

    #endregion

    #region Relation

    public ICollection<ProjectTechnologyMapping> ProjectTechnologyMappings { get; set; }

    #endregion
}