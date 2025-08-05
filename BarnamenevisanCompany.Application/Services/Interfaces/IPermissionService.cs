using BarnamenevisanCompany.Domain.ViewModels.Admin.Permission;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IPermissionService
{
    Task<bool> CheckUserPermissionAsync(int userId, string permissionName);

    Task<bool> CheckUserPermissionAsync(int userId, IEnumerable<string> permissionNames);   

    Task<IReadOnlyList<AdminPermissionViewModel>> GetPermissionsAsync();
}