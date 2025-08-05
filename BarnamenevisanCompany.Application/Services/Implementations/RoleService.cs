using BarnamenevisanCompany.Application.Cache;
using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.RoleMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Models.Permission;
using BarnamenevisanCompany.Domain.Models.Role;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Role;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class RoleService(
    IRoleRepository roleRepository,
    IUserRoleMappingRepository userRoleMappingRepository,
    IMemoryCacheService memoryCacheService,
    IUserRepository userRepository) : IRoleService
{
    public async Task<AdminFilterRolesViewModel> FilterAsync(AdminFilterRolesViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<Role>();

        #region Filter

        if (!string.IsNullOrEmpty(filter.Name))
            conditions.Add(c => EF.Functions.Like(c.Name, $"%{filter.Name.Trim()}%"));

        switch (filter.DeleteStatus)
        {
            case DeleteStatus.NotDeleted:
                conditions.Add(c => !c.IsDeleted);
                break;
            case DeleteStatus.All:
                break;
            case DeleteStatus.Deleted:
                conditions.Add(c => c.IsDeleted);
                break;
        }

        #endregion

        await roleRepository.FilterAsync(filter, conditions, c => c.MapToRoleViewModel());
        return filter;
    }

    public async Task<Result> CreateAsync(AdminCreateRoleViewModel model)
    {
        var role = model.MapToRole();

        await roleRepository.InsertAsync(role);
        await roleRepository.SaveChangesAsync();

        await memoryCacheService.RemoveAsync(CacheKeys.RolePermissionMappings);

        return Result.Success(SuccessMessages.SuccessfullyDone);
    }

    public async Task<Result> UpdateAsync(AdminUpdateRoleViewModel model)
    {
        if (model.Id < 2)
            return Result.Failure(ErrorMessages.OperationFailedError);

        var role = await roleRepository.GetByIdAsync(model.Id);

        if (role is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        model.MapToRole(role);

        roleRepository.Update(role);
        await roleRepository.SaveChangesAsync();

        await memoryCacheService.RemoveAsync(CacheKeys.RolePermissionMappings);

        return Result.Success(SuccessMessages.SuccessfullyDone);
    }

    public async Task<Result<AdminUpdateRoleViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateRoleViewModel>(ErrorMessages.OperationFailedError);

        var role = await roleRepository.GetByIdAsync(id);

        if (role is null)
            return Result.Failure<AdminUpdateRoleViewModel>(ErrorMessages.NotFoundError);

        var model = role.MapToUpdateRoleViewModel();

        return model;
    }

    public async Task<Result> DeleteOrRecoverAsync(short id)
    {
        if (id < 2)
            return Result.Failure(ErrorMessages.OperationFailedError);

        var role = await roleRepository.GetByIdAsync(id);

        if (role is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        roleRepository.SoftDeleteOrRecover(role);
        await roleRepository.SaveChangesAsync();

        await memoryCacheService.RemoveAsync(CacheKeys.RolePermissionMappings);

        return Result.Success(SuccessMessages.SuccessfullyDone);
    }

    public async Task<Result> SetPermissionToRoleAsync(AdminSetPermissionToRoleViewModel model)
    {
        if (model.RoleId < 1)
            return Result.Failure(ErrorMessages.OperationFailedError);

        await roleRepository.DeleteRolePermissionsByIdAsync(model.RoleId);
        var rolePermission = new List<RolePermissionMapping>();

        if (model.PermissionIds is not null && model.PermissionIds?.Count > 0)
        {
            foreach (var permissionId in model.PermissionIds)
            {
                rolePermission.Add(new(roleId: model.RoleId, permissionId: permissionId));
            }

            await roleRepository.InsertPermissionsToRoleAsync(rolePermission);
        }

        await roleRepository.SaveChangesAsync();

        await memoryCacheService.RemoveAsync(CacheKeys.RolePermissionMappings);

        return Result.Success(SuccessMessages.SavedChangesSuccessfully);
    }

    public async Task<List<short>> GetRoleSelectedPermissionAsync(short roleId) =>
        await roleRepository.GetSelectedPermission(roleId);

    public async Task<bool> IsUserAdminAsync(int userId) =>
        await roleRepository.AnyAsync(s => s.UserRoleMappings.Any(item => item.UserId == userId));

    public async Task<string> GetRoleIdsJoinByCommaAsync(int userId)
    {
        if (userId < 1) return string.Empty;

        var roleIds = await userRoleMappingRepository.GetAllAsync(s => s.RoleId, s => s.UserId == userId && !s.Role.IsDeleted);

        if (roleIds is null) return string.Empty;

        return string.Join(",", roleIds);
    }

    public async Task<Result> SetRoleToUserAsync(AdminSetRoleToUserViewModel model)
    {
        if (model.UserId < 1) return Result.Failure(ErrorMessages.SomethingWentWrong);

        var user = await userRepository.GetByIdAsync(model.UserId);

        if (user is null) return Result.Failure(ErrorMessages.UserNotFoundError);

        await userRoleMappingRepository.ExecuteDeleteRange(s => s.UserId == model.UserId);

        var roleIds = model.RoleIds?.ConvertStringToShortList();

        if (roleIds is not null && roleIds.Any())
        {
            var userRoleMappings = roleIds.Select(s => new UserRoleMapping
            {
                UserId = model.UserId,
                RoleId = s
            }).ToList();
            
            await userRoleMappingRepository.InsertRangeAsync(userRoleMappings);
            await userRoleMappingRepository.SaveChangesAsync();
        }

        await memoryCacheService.RemoveAsync(CacheKey.Format(CacheKeys.UserRoleMappings, user.Id));
        
        return Result.Success(SuccessMessages.SuccessfullyDone);
    }
}