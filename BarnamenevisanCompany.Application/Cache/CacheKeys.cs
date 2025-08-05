namespace BarnamenevisanCompany.Application.Cache;

public static class CacheKeys
{
    #region Permissions

    public static readonly CacheKey UserRoleMappings = new("UserRoleMappings-{0}");
    
    public static readonly CacheKey RolePermissionMappings = new("RolePermissionMappings");

    #endregion
}