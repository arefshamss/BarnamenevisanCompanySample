using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.Enums.Project;

public enum ProjectStatus
{
    [Display(Name = "بدون فیلتر")]
    None,
    [Display(Name = "اولویت نمایش")]
    Priority
}