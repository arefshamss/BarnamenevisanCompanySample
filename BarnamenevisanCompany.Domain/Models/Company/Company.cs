using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Company;

public sealed class Company : BaseEntity<short>
{
    #region Properties

    public required string Title { get; set; }
    public string? SiteUrl { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }

    #endregion
    
    #region Relations

    public ICollection<Honors.Honors> Honors { get; set; }

    #endregion
}