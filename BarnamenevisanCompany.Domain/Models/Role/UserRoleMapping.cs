using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Role;

public sealed class UserRoleMapping : BaseEntity
{
    #region Properties

    public int UserId { get; set; }
    
    public short RoleId { get; set; }   

    #endregion

    #region Relations

    public User.User User { get; set; }
    
    public Role Role { get; set; }  

    #endregion
}