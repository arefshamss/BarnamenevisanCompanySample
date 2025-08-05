using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Consult;

public class ClientCreateConsultViewModel:GoogleReCaptchaViewModel
{
    public int? UserId { get; set; }    
    
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Title { get; set; }
    
    [Display(Name = "نام")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(60, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string FirstName { get; set; }
    
    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(60, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string LastName { get; set; }    
    
    [Display(Name = "شماره همراه")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(11, ErrorMessage = ErrorMessages.MaxLengthError)]
    [RegularExpression(SiteRegex.MobileRegex, ErrorMessage = ErrorMessages.MobileNotValid)]
    public string Mobile { get; set; }  
    
    [Display(Name = "توضیحات درخواست")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Description { get; set; }
    
}