using BarnamenevisanCompany.Domain.Models.Role;

namespace BarnamenevisanCompany.Infra.Data.Seeds;

public static class RoleSeeds
{
    public static List<Role> Roles =
    [
        new()
        {
            Id = 1,
            Name = "ادمین",
            CreatedDate = SeedStaticDateTime.Date,
            IsDeleted = false
        }
    ];
}