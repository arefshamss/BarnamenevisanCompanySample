using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.SiteSetting;

public class AdminSiteSettingUpdateViewModel
{
    [Display(Name = "متن صفحه درخواست مشاوره")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string ConsultInformation { get; set; }

    [Display(Name = "متن صفحه تماس با ما")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string ContactUsHeaderText { get; set; }

    [Display(Name = "درباره ما فوتر")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(450, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string AboutUs { get; set; }

    [Display(Name = "تماس با ما فوتر")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string ContactUs { get; set; }

    [Display(Name = "متن صفحه اصلی")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string IndexParagraph { get; set; }

    [Display(Name = "عنوان صفحه اصلی")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string IndexTitle { get; set; }

    [Display(Name = "عنوان ثبت سفارش صفحه اصلی")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string IndexOrderTitle { get; set; }

    [Display(Name = "پاراگراف ثبت سفارش صفحه اصلی")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string IndexOrderParagraph { get; set; }

    [Display(Name = "پروژه‌های تکمیل شده")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [Range(0, short.MaxValue, ErrorMessage = ErrorMessages.RangeError)]
    public short CompletedProject { get; set; }

    [Display(Name = "لوگو سایت")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string SiteLogoImageName { get; set; }

    [Display(Name = "FavIcon")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string SiteFavIconImageName { get; set; }

    [Display(Name = "لوگو سایت")] public IFormFile? SiteLogo { get; set; }
    [Display(Name = "آیکون مرورگر سایت")] public IFormFile? SiteFavIcon { get; set; }

    [Display(Name = "عنوان صفحه لیست موقعیت های شغلی")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(100, ErrorMessage = ErrorMessages.MaxLengthError)]
    public required string JobListTitle { get; set; }

    [Display(Name = "توضیحات صفحه لیست موقعیت های شغلی")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public required string JobListDescription { get; set; }

    [Display(Name = "تصویر لیست موقعیت های شغلی")]
    public IFormFile? JobListImage { get; set; }    
    public string JobListImageName { get; set; }
    
    [Display(Name = "عنوان صفحه خدمات ما")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(100, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string OurServicePageTitle { get; set; } 
    
    [Display(Name = "توضیحات صفحه خدمات ما")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string OurServiceDescription { get; set; }
    
    [Display(Name = "متن پیامک درخواست مشاوره")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public required string ConsultSmsMessage { get; set; }     
    
    [Display(Name = "شماره همراه اطلاع رسانی")]
    [MaxLength(11, ErrorMessage = ErrorMessages.MaxLengthError)]
    [RegularExpression(SiteRegex.MobileRegex, ErrorMessage = ErrorMessages.MobileNotValid)]
    public string? NotificationPhoneNumber { get; set; }  
}