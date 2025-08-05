using BarnamenevisanCompany.Domain.Models.Role;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Role;

namespace BarnamenevisanCompany.Application.Mappers.RoleMappings;

public static class RoleMapper
{
    public static AdminRoleViewModel MapToRoleViewModel(this Role role) => new()
    {
        Id = role.Id,
        IsDeleted = role.IsDeleted,
        Name = role.Name
    };

    public static Role MapToRole(this AdminCreateRoleViewModel model) => new()
    {
        Name = model.Name
    };

    public static AdminUpdateRoleViewModel MapToUpdateRoleViewModel(this Role role) => new()
    {
        Name = role.Name,
        Id = role.Id
    };

    public static void MapToRole(this AdminUpdateRoleViewModel model, Role role)
    {
        role.Name = model.Name;
    }
}