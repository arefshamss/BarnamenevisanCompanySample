using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.ContactUs;

public sealed class ContactUsInformation : BaseEntity<short>
{
    #region Properties

    public required string Email { get; set; }

    public required string Managment { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Address { get; set; }

    public required string Latitude { get; set; }

    public required string Longitude { get; set; }

    #endregion
}