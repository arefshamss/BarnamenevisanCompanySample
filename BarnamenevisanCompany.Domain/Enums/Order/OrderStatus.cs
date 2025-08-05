using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.Enums.Order;

public enum OrderStatus
{
    [Display(Name = "در حال بررسی")] InProgress,

    [Display(Name = "رد شده")] Rejected,

    [Display(Name = "پاسخ داده شده")] Done,
}

public enum FilterOrderStatus
{
    [Display(Name = "در حال بررسی")] InProgress,
    
    [Display(Name = "همه")] All,

    [Display(Name = "رد شده")] Rejected,

    [Display(Name = "پاسخ داده شده")] Done,
}

public enum OrderStepFilter
{
    [Display(Name = "بدون فیلتر")]
    None,
    
    [Display(Name = "اولویت نمایش")]
    Priority
}