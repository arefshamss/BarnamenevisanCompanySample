using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.ContactUsInformation;

public class AdminUpdateContactUsInformationViewModel
{
    public short Id { get; set; }

    [MaxLength(150, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [RegularExpression(SiteRegex.EmailRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string Email { get; set; }

    [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Display(Name = "مدیریت")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Managment { get; set; }

    [MaxLength(150, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Display(Name = "تلفن")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string PhoneNumber { get; set; }

    [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Display(Name = "آدرس")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Address { get; set; }

    [Display(Name = "عرض جغرافیایی")] public string Latitude { get; set; }
    [Display(Name = " طول جغرافیایی")] public string Longitude { get; set; }
}