using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Blog;

public class ClientBlogDetailsViewModel
{
    public int Id { get; set; }
    
    
    [Display(Name = "نویسنده")]
    public string? Author { get; set; }
    
    
    [Display(Name = "عنوان")]
    public string Title { get; set; }

    
    [Display(Name = "توضیحات کوتاه")]
    public string ShortDescription { get; set; }

    
    [Display(Name = "متن اصلی")]
    public string Description { get; set; }

    
    [Display(Name = "تصویر")]
    public string ImageUrl { get; set; }

    
    [Display(Name="تعداد بازدید")]
    public int VisitCount { get; set; }
    
    public DateTime CreatedDate { get; set; }
}