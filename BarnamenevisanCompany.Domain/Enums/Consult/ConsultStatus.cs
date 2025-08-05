using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.Enums.Consult;

public enum ConsultStatus : byte
{
    [Display(Name = "همه")]
    All,
    [Display(Name = "بدون پاسخ")]
    InProcess,
    [Display(Name = "پاسخ داده شده")]
    IsAnswered,
}