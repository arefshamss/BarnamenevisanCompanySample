using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Consult;

public class AdminConsultDetailViewModel
{
    public short Id { get; set; }

    public int? UserId { get; set; }    

    [Display(Name = "عنوان")] 
    public string Title { get; set; }

    [Display(Name = "نام و نام خانوادگی")] 
    public string UserFullName { get; set; }

    [Display(Name = "تاریخ ایجاد")] 
    public string CreateDate { get; set; }

    [Display(Name = "توضیحات")] 
    public string Description { get; set; }

    [Display(Name = "شماره تلفن")]
    public string Mobile { get; set; }

    public string? AdminAnswer { get; set; }
}