using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Enums.Job;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Job;

public class AdminCreateJobViewModel
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Title { get; set; }

    
    [Display(Name = "آدرس صفحه")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(300,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Slug { get; set; }
    
    
    [Display(Name = "توضیحات کوتاه")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(700, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string ShortDescription { get; set; }

    
    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Description { get; set; }

    
    [Display(Name = "حداقل حقوق (ریال)")]
    [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? SalaryFrom { get; set; }
    
    
    [Display(Name = "حداکثر حقوق (ریال)")]
    [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? SalaryTo { get; set; }
    
    
    [Display(Name = "سابقه کار")]
    public string? WorkExperience { get; set; }

    
    [Display(Name = "تاریخ انقضا")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string ExpireDate { get; set; }   
    
    
    [Display(Name = "شرایط کار")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public JobConditionsStatus JobConditions { get; set; }

    
    [Display(Name = "مهارت‌ها")]
    public string? Skills { get; set; }

    
    [Display(Name = "آدرس")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1000, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Address { get; set; }
    
    public string? Latitude { get; set; }

    public string? Longitude { get; set; }
}