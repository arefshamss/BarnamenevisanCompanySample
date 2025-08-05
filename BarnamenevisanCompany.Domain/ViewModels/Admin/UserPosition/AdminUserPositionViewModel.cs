using BarnamenevisanCompany.Domain.ViewModels.Admin.User;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.UserPosition;

public class AdminUserPositionViewModel
{
    public short Id { get; set; }
    
    public int UserId { get; set; }

    public string Mobile { get; set; }
    
    public short Priority { get; set; }
    
    public string Position { get; set; }

    public string? WebSiteAddress { get; set; } 

    public string UserFullName { get; set; }

    public string? UserAvatarImageName { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreateDate { get; set; }
    
}