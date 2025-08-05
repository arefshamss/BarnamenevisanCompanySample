using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.Enums.Common;

public enum DeleteStatus : byte
{
    [Display(Name = "همه")]
    All = 1,
    
    [Display(Name = "حذف شده")]
    Deleted = 2,
    
    [Display(Name = "حذف نشده")]
    NotDeleted = 0
}