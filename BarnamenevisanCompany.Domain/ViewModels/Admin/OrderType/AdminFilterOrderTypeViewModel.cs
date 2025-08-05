using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.OrderType;

public class AdminFilterOrderTypeViewModel : BasePaging<AdminOrderTypeViewModel>
{
    [Display(Name="عنوان"), FilterInput]
    public string? Title { get; set; }
    
    
    [Display(Name="وضعیت حذف"),FilterInput]
    public DeleteStatus DeleteStatus { get; set; }
}