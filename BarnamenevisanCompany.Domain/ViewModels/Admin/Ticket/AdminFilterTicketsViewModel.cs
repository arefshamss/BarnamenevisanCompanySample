using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Ticket;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Ticket;

public class AdminFilterTicketsViewModel : BasePaging<AdminTicketViewModel>
{
    [FilterInput]
    [Display(Name = "موضوع")]
    public string? Title { get; set; }   
    
    [FilterInput]
    [Display(Name = "کاربر")]
    public int? UserId { get; set; }      
    
    [FilterInput]
    [Display(Name = "اولویت")]
    public int? PriorityId { get; set; }

    [FilterInput]
    [Display(Name = "وضعیت تیکت")]
    public FilterByTicketStatus TicketStatus { get; set; }    
    
    [FilterInput]
    [Display(Name = "مرتب سازی بر اساس")]   
    public FilterOrderBy FilterOrderBy { get; set; }
    
    [FilterInput]
    [Display(Name = "وضعیت حذف")]   
    public DeleteStatus DeleteStatus { get; set; }
}