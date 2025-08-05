using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Order;

public class AdminFilterOrderViewModel : BasePaging<AdminOrderViewModel>
{
    [Display(Name="عنوان سفارش"),FilterInput]
    public string? Title { get; set; }
    
    
    [Display(Name="شماره تماس"),FilterInput]
    public string? Mobile { get; set; }
    
    
    [Display(Name="سفارش‌دهنده"), FilterInput]
    public int? UserId { get; set; }
    public string? UserName { get; set; }
    
    
    [Display(Name = "مرتب سازی بر اساس"), FilterInput]
    public FilterOrderBy FilterOrderBy { get; set; }
    
    
    [Display(Name="وضعیت حذف"), FilterInput]
    public DeleteStatus DeleteStatus { get; set; }   
    
    
    [Display(Name="وضعیت سفارش"), FilterInput]
    public FilterOrderStatus OrderStatus { get; set; }
}