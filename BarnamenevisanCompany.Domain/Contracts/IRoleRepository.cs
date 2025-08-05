using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.Permission;
using BarnamenevisanCompany.Domain.Models.Role;

namespace BarnamenevisanCompany.Domain.Contracts;

public interface IRoleRepository : IEfRepository<Role, short>
{
    Task InsertPermissionsToRoleAsync(List<RolePermissionMapping> permissions);

    Task DeleteRolePermissionsByIdAsync(short roleId);

    Task<List<short>> GetSelectedPermission(short roleId);
}