using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.OrderStep;

public class AdminFilterOrderStepViewModel : BasePaging<AdminOrderStepViewModel>
{
    [FilterInput]
    [Display(Name = "عنوان")]
    public string? Title { get; set; }

    [FilterInput]
    [Display(Name = "بر اساس نمایش")]
    public OrderStepFilter OrderStepFilter { get; set; }

    [FilterInput]
    [Display(Name = "وضعیت حذف")]
    public DeleteStatus DeleteStatus { get; set; }
}