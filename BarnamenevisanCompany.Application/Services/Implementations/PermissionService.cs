using BarnamenevisanCompany.Application.Cache;
using BarnamenevisanCompany.Application.Mappers.PermissionMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Role;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Permission;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class PermissionService(
    IPermissionRepository permissionRepository,
    IUserRoleMappingRepository userRoleMappingRepository,
    IMemoryCacheService memoryCacheService) : IPermissionService
{
    public async Task<bool> CheckUserPermissionAsync(int userId, string permissionName)
    { 
        var userRoleMapping = await memoryCacheService.GetOrCreateAsync(
            CacheKey.Format(CacheKeys.UserRoleMappings, userId),
            async () => await userRoleMappingRepository.GetAllAsync(c => c.UserId == userId, includes: nameof(UserRoleMapping.Role)));

        if (userRoleMapping is null || userRoleMapping.Count == 0) return false;

        var rolePermissionMappings = await memoryCacheService.GetOrCreateAsync(CacheKeys.RolePermissionMappings,
            async () => await permissionRepository.GetAllRolePermissions());

        return userRoleMapping.Any(s =>
            rolePermissionMappings.Any(p => p.RoleId == s.RoleId &&
                                            p.Permission.UniqueName == permissionName));
    }

    public async Task<bool> CheckUserPermissionAsync(int userId, IEnumerable<string> permissionNames)
    {
        List<bool> permissions = [];
        foreach (var permissionName in permissionNames ?? []) permissions.Add(await CheckUserPermissionAsync(userId, permissionName));
        return permissions.Any(hasAccess => hasAccess);
    }

    public async Task<IReadOnlyList<AdminPermissionViewModel>> GetPermissionsAsync()
    {
        var permissions = await permissionRepository.GetAllAsync();
        return permissions.Select(s => s.MapToPermissionViewModel()).ToList();
    }
}