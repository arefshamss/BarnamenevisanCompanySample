using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.PartnerCompany;

public class AdminPartnerCompanyViewModel
{
    [Display(Name = "شناسه")] public short Id { get; set; }
    
    [Display(Name = "عنوان سایت")] public string Title { get; set; }
    
    [Display(Name = "تصویر")] public string? ImageUrl { get; set; }

    
    [Display(Name = "آدرس سایت")] public string? SiteUrl { get; set; }
    
    public bool IsDeleted { get; set; }
}