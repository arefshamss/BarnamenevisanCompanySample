using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.SiteSetting;

public sealed class SiteSetting : BaseEntity<byte>
{
    #region Properties

    public required string ConsultInformationText { get; set; }
    public required string ContactUsHeaderText { get; set; }
    public required string AboutUsFooter { get; set; }
    public required string ContactUsFooter { get; set; }
    public short CompletedProject { get; set; }

    public string IndexTitle { get; set; }
    public string IndexParagraph { get; set; }
    public string IndexOrderTitle { get; set; }
    public string IndexOrderParaghraph { get; set; }

    public required string SiteLogo { get; set; }
    public required string FavIcon { get; set; }

    public required string JobListTitle { get; set; }
    public required string JobListDescription { get; set; }
    public required string JobListImageName { get; set; }

    public required string OurServicePageTitle { get; set; }
    public required string OurServiceDescription { get; set; }

    public required string ConsultSmsMessage { get; set; }

    public string? NotificationPhoneNumber { get; set; }

    #endregion
}