using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OrderType;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Order;

public class AdminOrderViewModel
{
    [Display(Name = "شناسه")]
    public int Id { get; set; }
    
    
    [Display(Name = "عنوان")]
    public string Title { get; set; }
    
    
    [Display(Name = "سفارش‌دهنده")]
    public string CustomerName { get; set; }
    
    
    [Display(Name = "شماره تماس")]
    public string CustomerMobile { get; set; }
    
    
    [Display(Name = "تاریخ سفارش")]
    public DateTime OrderDate { get; set; }
    
    
    [Display(Name = "وضعیت سفارش")]
    public OrderStatus OrderStatus { get; set; }
    
    
    [Display(Name = "نوع سفارش")]
    public List<AdminOrderTypeDetailsViewModel> OrderTypes { get; set; } 
    
    
    public bool IsDeleted { get; set; }
}