using BarnamenevisanCompany.Domain.Models.AboutUs;
using BarnamenevisanCompany.Domain.Models.Blog;
using BarnamenevisanCompany.Domain.Models.Common;
using BarnamenevisanCompany.Domain.Models.Job;
using BarnamenevisanCompany.Domain.Models.Project;
using BarnamenevisanCompany.Domain.Models.Role;
using BarnamenevisanCompany.Domain.Models.Ticket;

namespace BarnamenevisanCompany.Domain.Models.User;

public sealed class User : BaseEntity
{
    #region Properties

    public string? AvatarImageName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Mobile { get; set; }
    public string? Password { get; set; }
    public bool IsActive { get; set; }
    public string? ActiveCode { get; set; }
    public DateTime? ActiveCodeExpireTime { get; set; }

    #endregion

    #region Relations

    public ICollection<UserPosition.UserPosition> UserPositions { get; set; }
    public ICollection<Ticket.Ticket> Tickets { get; set; }
    public ICollection<TicketMessage> TicketMessages { get; set; }
    public ICollection<Blog.Blog> Blogs { get; set; }
    public ICollection<BlogUserMapping> BlogUserMappings { get; set; }
    public ICollection<ProjectVisitsMapping> ProjectVisitsMappings { get; set; }
    public ICollection<UserSocialNetworkMapper.UserSocialNetworkMapping> UserSocialNetworkMappings { get; set; }
    public ICollection<Consult.Consult> Consults { get; set; }
    public ICollection<Order.Order> Orders { get; set; }
    public ICollection<JobUserMapping> JobUserMappings { get; set; }
    public ICollection<UserRoleMapping> UserRoleMappings { get; set; }  
    public ICollection<AboutUsComment> AboutUsComments { get; set; }  

    #endregion
}