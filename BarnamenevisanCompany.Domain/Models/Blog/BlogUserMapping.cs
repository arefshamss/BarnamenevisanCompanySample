using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Blog;

public sealed class BlogUserMapping : BaseEntity
{
    #region Properties

    public int BlogId { get; set; }

    public int? UserId { get; set; }

    public required string UserIp { get; set; } 

    #endregion

    #region Relations

    public Blog Blog { get; set; }

    public User.User? User { get; set; }

    #endregion
}