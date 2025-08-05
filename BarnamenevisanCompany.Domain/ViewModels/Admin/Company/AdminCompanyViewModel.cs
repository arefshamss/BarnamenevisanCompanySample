using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Company;

public class AdminCompanyViewModel
{
    [Display(Name = "شناسه")] public short Id { get; set; }

    [Display(Name = "نام شرکت")] public string Title { get; set; }

    [Display(Name = "تصویر")] public string? ImageUrl { get; set; }

    
    [Display(Name = "آدرس سایت")] public string? SiteUrl { get; set; }

    public bool IsDeleted { get; set; }
}