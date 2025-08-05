using BarnamenevisanCompany.Domain.Models.Common;
using BarnamenevisanCompany.Domain.Models.Permission;

namespace BarnamenevisanCompany.Domain.Models.Role;

public sealed class Role : BaseEntity<short>    
{
    #region Properties

    public string Name { get; set; }    

    #endregion

    #region Relations

    public ICollection<RolePermissionMapping> RolePermissionMappings { get; set; }  
    public ICollection<UserRoleMapping> UserRoleMappings { get; set; }  

    #endregion
}