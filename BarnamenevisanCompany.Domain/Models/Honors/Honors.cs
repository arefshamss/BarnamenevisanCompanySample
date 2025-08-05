using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Honors;

public sealed class Honors : BaseEntity<short>
{
    #region Properties

    public required string Title { get; set; }

    public required string Slug { get; set; }

    public required string ImageName { get; set; }

    public string? TeaserName { get; set; }

    public string? TeaserPoster { get; set; }

    public string? Description { get; set; }

    public short CompanyId { get; set; }

    #endregion

    #region Relation

    public ICollection<HonorsGallery> HonorsGalleries { get; set; }

    public Company.Company Company { get; set; }

    #endregion
}