using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.AboutUsComment;

public class AdminFilterAboutUsCommentViewModel:BasePaging<AdminAboutUsCommentViewModel>
{
    [FilterInput]
    [Display(Name = "فلیتر بر اساس نمره کامنت")]
    public byte? Rating { get; set; }
    [FilterInput]
    [Display(Name = "حذف")]
    public DeleteStatus DeleteStatus { get; set; }
}