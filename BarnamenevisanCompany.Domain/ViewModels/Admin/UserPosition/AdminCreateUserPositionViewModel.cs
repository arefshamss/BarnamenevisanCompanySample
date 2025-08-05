using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.UserPosition;

public class AdminCreateUserPositionViewModel
{
    public int UserId { get; set; }

    [Display(Name = "سمت کاربر")]
    [MaxLength(150, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Position { get; set; }

    [Display(Name = "آدرس سایت رزومه")]
    [MaxLength(150, ErrorMessage = ErrorMessages.MaxLengthError)]
    [RegularExpression(SiteRegex.SiteUrlRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string? WebsiteAddress { get; set; }


    [Range(1, 100, ErrorMessage = ErrorMessages.RangeError)]
    [Display(Name = "اولویت نمایش")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public short Priority { get; set; }
}