using System.Collections;
using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Project;

public sealed class Project : BaseEntity<short>
{
    #region Properties

    public required string Title { get; set; }

    public required string SiteUrl { get; set; }

    public required string ImageName { get; set; }

    public required string Slug { get; set; }

    public string? TeaserName { get; set; }


    public string? TeaserPoster { get; set; }

    public byte ProgressRate { get; set; }

    public required string Description { get; set; }

    public short Priority { get; set; }

    #endregion

    #region Relations

    public ICollection<ProjectGallery> ProjectGalleries { get; set; }

    public ICollection<ProjectMemberMapping> ProjectMemberMappings { get; set; }

    public ICollection<ProjectVisitsMapping> ProjectVisitsMappings { get; set; }

    public ICollection<ProjectTechnologyMapping> ProjectTechnologyMappings { get; set; }

    #endregion
}