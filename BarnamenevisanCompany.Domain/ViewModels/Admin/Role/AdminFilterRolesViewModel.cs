using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Role;

public class AdminFilterRolesViewModel : BasePaging<AdminRoleViewModel>
{
    [Display(Name = "عنوان")]
    [FilterInput]
    public string? Name { get; set; }

    [Display(Name = "وضعیت حذف")]
    [FilterInput]
    public DeleteStatus DeleteStatus { get; set; }
}