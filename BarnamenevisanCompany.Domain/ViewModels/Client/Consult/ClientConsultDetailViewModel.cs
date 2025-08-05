using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Consult;

public class ClientConsultDetailViewModel
{
    [Display(Name = "عنوان ")] 
    
    public string Title { get; set; }

    [Display(Name = "درخواست")] 
    
    public string Descriptrion { get; set; }

    [Display(Name = "پاسخ ادمین")] 
    
    public string AdminAnswer { get; set; }

    [Display(Name = "نام و نام خانوادگی")]
    
    public string UserFullName { get; set; }
    
    [Display(Name = "تاریخ ایجاد")] 
    
    public DateTime CreateDate { get; set; }
}