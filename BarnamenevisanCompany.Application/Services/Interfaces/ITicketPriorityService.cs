using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.TicketPriority;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface ITicketPriorityService
{
    Task<Result<AdminFilterTicketPrioritiesViewModel>> FilterAsync(AdminFilterTicketPrioritiesViewModel filter);    
    Task<Result<AdminUpdateTicketPriorityViewModel>> FillModelForUpdateAsync(short id);
    Task<Result<SelectList>> GetSelectListAsync();   
    
    Task<Result> CreateAsync(AdminCreateTicketPriorityViewModel model);
    Task<Result> UpdateAsync(AdminUpdateTicketPriorityViewModel model);
    Task<Result> DeleteOrRecoverAsync(short id);
}   