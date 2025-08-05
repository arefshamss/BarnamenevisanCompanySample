using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Blog;

public class AdminBlogViewModel
{
    [Display(Name = "شناسه")] public int Id { get; set; }

    [Display(Name = "نام شرکت")] public string Title { get; set; }

    [Display(Name = "نویسنده")] public string? Author { get; set; }

    [Display(Name = "تاریخ ایجاد")] public DateTime CreatedDate { get; set; }

    public bool IsDeleted { get; set; }
}