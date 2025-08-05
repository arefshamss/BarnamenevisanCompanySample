using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.PartnerCompany;

public class ClientPartnerCompanyViewModel
{
    [Display(Name = "شناسه")] public short Id { get; set; }
    
    [Display(Name = "عنوان سایت")] public string Title { get; set; }
    
    [Display(Name = "تصویر")] public string? ImageUrl { get; set; }
    
    [Display(Name = "آدرس سایت")] public string? SiteUrl { get; set; }
}