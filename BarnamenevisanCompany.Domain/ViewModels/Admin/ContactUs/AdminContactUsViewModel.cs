using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.ContactUs;

public class AdminContactUsViewModel
{
    public short Id { get; set; }
    [Display(Name = "عنوان")]
    public string Title { get; set; }
    
    [Display(Name = "نام و نام خانوادگی")]
    public string FullName { get; set; }
    
    [Display(Name = "شماره تماس")]
    [RegularExpression(SiteRegex.MobileRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string PhoneNumber { get; set; }
    
    [Display(Name = "ایمیل")]
    [RegularExpression(SiteRegex.EmailRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string Email { get; set; }

    
    public string AdminMessage { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }

    
}