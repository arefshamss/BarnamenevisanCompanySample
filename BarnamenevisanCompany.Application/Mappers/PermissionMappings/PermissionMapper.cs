using BarnamenevisanCompany.Domain.Models.Permission;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Permission;

namespace BarnamenevisanCompany.Application.Mappers.PermissionMappings;

public static class PermissionMapper
{
    public static AdminPermissionViewModel MapToPermissionViewModel(this Permission permission) => new()
    {
        Id = permission.Id,
        DisplayName = permission.DisplayName,
        ParentId = permission.ParentId
    };
}