using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.TicketPriority;

public class AdminFilterTicketPrioritiesViewModel : BasePaging<AdminTicketPriorityViewModel>
{
    [FilterInput]
    [Display(Name = "عنوان")]
    public string? Title { get; set; }
    
    [FilterInput]
    [Display(Name = "مرتب سازی بر اساس")]      
    public FilterOrderBy FilterOrderBy { get; set; }
    
    [FilterInput]
    [Display(Name = "وضعیت حذف")]   
    public DeleteStatus DeleteStatus { get; set; }
}