using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.AboutUs;

public class AboutUsComment : BaseEntity<short>
{
    #region Properties

    public required string Comment { get; set; }
    public byte Rating { get; set; }
    public int UserId { get; set; }

    #endregion

    #region Relations
    public User.User Users { get; set; }

    #endregion
}