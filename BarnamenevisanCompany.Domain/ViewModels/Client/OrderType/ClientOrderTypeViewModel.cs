using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.OrderType;

public class ClientOrderTypeViewModel
{
    public short Id { get; set; }
    
    
    [Display(Name = "عنوان")]
    public string Title { get; set; }
    
    
    [Display(Name = "توضیحات")]
    public string? Description { get; set; }
    
    
    [Display(Name = "تصویر")]
    public string? ImageUrl { get; set; }
}