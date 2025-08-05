using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.ContactUs;

public sealed class ContactUs : BaseEntity<short>
{
    #region Properties  

    public required string Title { get; set; }

    public required string FullName { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Email { get; set; }

    public required string Message { get; set; }

    public string? AdminMessage { get; set; }

    #endregion
}