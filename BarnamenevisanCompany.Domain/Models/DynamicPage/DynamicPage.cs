using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.DynamicPage;

public sealed class DynamicPage : BaseEntity<short>
{
    #region Properties

    public required string Title { get; set; }
    
    public required string Slug { get; set; } 
    
    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    #endregion
}