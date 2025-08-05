using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Permission;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class PermissionRepository(BarnamenevisanContext context) : EfRepository<Permission, short>(context), IPermissionRepository
{
    public async Task<List<RolePermissionMapping>> GetAllRolePermissions() => 
        await context.RolePermissionMappings
            .Include(s => s.Permission)
            .Include(c => c.Role)
            .ToListAsync();
}