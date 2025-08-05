using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Project;

public class ProjectGallery : BaseEntity<short>
{
    #region Properties  

    public required string ImageName { get; set; }

    #endregion

    #region Relations

    public short ProjectId { get; set; }
    public Project Project { get; set; }

    #endregion
}