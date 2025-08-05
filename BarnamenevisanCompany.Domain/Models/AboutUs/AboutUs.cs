using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.AboutUs;

public sealed class AboutUs : BaseEntity<short>
{
    #region Properties
    public required string TopTitle { get; set; }

    public required string TopDescription { get; set; }
    
    public required string MainDescriptionLeft { get; set; }
    
    public required string MainDescriptionRight { get; set; }
    
    public required string OurValuesTitle { get; set; }
    
    public required string OurPassionDescription { get; set; }    
    
    public required string TransparencyDescription { get; set; }
    
    public required string OurMissionDescription { get; set; }
    public required string OurValuesDescription { get; set; }
    
    
    
    #endregion
}