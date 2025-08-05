using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Faq;

public sealed class Faq : BaseEntity<short>
{
    #region Properties

    public required string Question { get; set; }

    public required string Answer { get; set; }

    #endregion
}