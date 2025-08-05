using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OrderType;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Order;

public class AdminOrderDetailsViewModel
{
    [Display(Name = "شناسه")]
    public int Id { get; set; }
    
    
    [Display(Name = "عنوان")]
    public string Title { get; set; }    
    
    
    [Display(Name = "توضیحات")]
    public string Description { get; set; }
    
    
    [Display(Name="ضمیمه")]
    public string Attachment { get; set; }
    
    
    [Display(Name = "سفارش‌دهنده")]
    public string CustomerName { get; set; }
    
    
    [Display(Name = "شماره تماس")]
    public string CustomerMobile { get; set; }
    
    
    [Display(Name = "تاریخ سفارش")]
    public DateTime OrderDate { get; set; }
    
    
    [Display(Name = "وضعیت سفارش")]
    public OrderStatus OrderStatus { get; set; }       
    
    
    [Display(Name = "شماره سفارش")]
    public string OrderNumber { get; set; }    
    
    
    [Display(Name = "نوع سفارش")]
    public List<AdminOrderTypeDetailsViewModel> OrderTypes { get; set; } 
    
    
    public string? Answer { get; set; } 
}