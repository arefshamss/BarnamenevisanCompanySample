using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Permission;

public sealed class RolePermissionMapping(short roleId, short permissionId)
{
    #region Properties

    public short RoleId { get; set; } = roleId;
    public short PermissionId { get; set; } = permissionId;

    #endregion

    #region Relations

    public Role.Role Role { get; set; }
    public Permission Permission { get; set; }  

    #endregion
}