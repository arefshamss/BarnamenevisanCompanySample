using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.OrderType;

public class AdminOrderTypeViewModel
{
    [Display(Name = "شناسه")] public short Id { get; set; }
    
    [Display(Name = "عنوان سایت")] public string Title { get; set; }
    
    [Display(Name = "تصویر")] public string? ImageUrl { get; set; }

    [Display(Name = "تاریخ ایجاد")] public DateTime CreatedDate { get; set; }
    
    public bool IsDeleted { get; set; }
}