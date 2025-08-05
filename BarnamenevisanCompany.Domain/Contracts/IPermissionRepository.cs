using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.Permission;

namespace BarnamenevisanCompany.Domain.Contracts;

public interface IPermissionRepository : IEfRepository<Permission, short>
{
    Task<List<RolePermissionMapping>> GetAllRolePermissions();
}