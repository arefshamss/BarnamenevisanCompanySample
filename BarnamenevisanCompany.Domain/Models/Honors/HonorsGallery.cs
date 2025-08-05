using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Honors;

public sealed class HonorsGallery:BaseEntity<short>
{
    #region Properties

    public required string ImageName { get; set; }

    public short HonorsId { get; set; }

    #endregion

    #region Relations

    public Honors Honors { get; set; }

    #endregion
}