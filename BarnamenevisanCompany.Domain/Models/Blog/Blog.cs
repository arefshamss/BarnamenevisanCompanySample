using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Blog;

public sealed class Blog : BaseEntity
{
    #region Properties

    public required string Title { get; set; }

    public required string Slug { get; set; }

    public required string ShortDescription { get; set; }

    public required string Description { get; set; }

    public string ImageUrl { get; set; }

    public int? UserId { get; set; }

    #endregion

    #region Relations

    public User.User? User { get; set; }
    
    public ICollection<BlogUserMapping> BlogUserMappings { get; set; }

    #endregion
}