using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.Enums.User;

public enum UserActiveStatus : byte
{
    [Display(Name = "فعال")]
    Active,
    [Display(Name = "همه")]
    All,
    [Display(Name = "غیرفعال")]
    Disabled,
}

public enum UserPositionStatus : byte
{

    [Display(Name = "اولویت نمایش")]
    DisplayPriority,
    [Display(Name = "بدون فیلتر")]
    None,
}