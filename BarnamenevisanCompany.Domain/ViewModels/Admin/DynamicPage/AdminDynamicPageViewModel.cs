using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.DynamicPage;

public class AdminDynamicPageViewModel
{
    public short Id { get; set; }


    [Display(Name = "عنوان")] public string Title { get; set; }


    [Display(Name = "وضعیت صفحه")] public bool IsActive { get; set; }


    [Display(Name = "تاریخ ایجاد")] public DateTime CreatedDate { get; set; }


    public bool IsDeleted { get; set; }
}