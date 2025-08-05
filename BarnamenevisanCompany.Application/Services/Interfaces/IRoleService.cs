using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Role;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IRoleService
{
    #region Role

    Task<AdminFilterRolesViewModel> FilterAsync(AdminFilterRolesViewModel filter);

    Task<Result<AdminUpdateRoleViewModel>> FillModelForUpdateAsync(short id);
    
    Task<string> GetRoleIdsJoinByCommaAsync(int userId);

    Task<Result> CreateAsync(AdminCreateRoleViewModel model);

    Task<Result> UpdateAsync(AdminUpdateRoleViewModel model);

    Task<Result> DeleteOrRecoverAsync(short id);

    #endregion

    #region RolePermission

    Task<Result> SetPermissionToRoleAsync(AdminSetPermissionToRoleViewModel model);

    Task<List<short>> GetRoleSelectedPermissionAsync(short roleId);

    #endregion

    #region UserRole

    Task<bool> IsUserAdminAsync(int userId);

    Task<Result> SetRoleToUserAsync(AdminSetRoleToUserViewModel model);
    
    #endregion
}