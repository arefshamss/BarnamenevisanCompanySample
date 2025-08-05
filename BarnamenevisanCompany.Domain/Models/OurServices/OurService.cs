using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.OurServices;

public sealed class OurService : BaseEntity<short>
{
    #region Properties

    public required string Title { get; set; }

    public required string ImageName { get; set; }

    public required string ShortDescription { get; set; }

    public required string Slug { get; set; }

    public required string LongDescription { get; set; }

    #endregion
}