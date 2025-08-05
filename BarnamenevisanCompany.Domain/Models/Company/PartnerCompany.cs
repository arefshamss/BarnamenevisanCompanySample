using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Company;

public sealed class PartnerCompany : BaseEntity<short>
{
    #region Properties

    public required string Title { get; set; }
    
    public string? SiteUrl { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public string? ShortDescription { get; set; }

    #endregion
}