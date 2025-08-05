using BarnamenevisanCompany.Domain.ViewModels.Admin.Permission;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Role;

public class AdminSetPermissionToRoleViewModel
{
    public short RoleId { get; set; }
    
    public List<short>? PermissionIds { get; set; }
    
    public List<short>? SelectedPermissionIds { get; set; }
    
    public IReadOnlyList<AdminPermissionViewModel>? Permissions { get; set; }   
}