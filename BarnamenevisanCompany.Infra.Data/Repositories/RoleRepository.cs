using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Permission;
using BarnamenevisanCompany.Domain.Models.Role;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class RoleRepository(BarnamenevisanContext context) : EfRepository<Role, short>(context), IRoleRepository
{
    public async Task InsertPermissionsToRoleAsync(List<RolePermissionMapping> permissions) =>
        await context.RolePermissionMappings.AddRangeAsync(permissions);

    public async Task DeleteRolePermissionsByIdAsync(short roleId) =>
        await context.RolePermissionMappings.Where(s => s.RoleId == roleId).ExecuteDeleteAsync();

    public async Task<List<short>> GetSelectedPermission(short roleId) =>
        await context.RolePermissionMappings.Where(s => s.RoleId == roleId)
            .Select(s => s.PermissionId).ToListAsync();
}