using BarnamenevisanCompany.Domain.Models.Ticket;
using BarnamenevisanCompany.Domain.ViewModels.Admin.TicketPriority;
using BarnamenevisanCompany.Domain.ViewModels.Client.TicketPriority;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarnamenevisanCompany.Application.Mappers.TicketPriorityMappings;

public static class TicketPriorityMapper
{
    #region Admin

    public static SelectListItem MapToSelectListItem(this TicketPriority model) =>
        new()
        {
            Text = model.Title,
            Value = model.Id.ToString()
        };
    
    public static AdminTicketPriorityViewModel MapToAdminTicketPriorityViewModel(this TicketPriority model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            IsDeleted = model.IsDeleted,
            CreatedDate = model.CreatedDate,
        };  
    
    public static TicketPriority MapToTicketPriority(this AdminCreateTicketPriorityViewModel model) => 
        new()
        {
            Title = model.Title,
        };

    public static AdminUpdateTicketPriorityViewModel MapToAdminUpdateTicketPriorityViewModel(this TicketPriority model) => 
        new()
        {
            Id = model.Id,
            Title = model.Title
        };
    
    public static void MapToTicketPriority(this TicketPriority ticketPriority, 
        AdminUpdateTicketPriorityViewModel model)   
    {
        ticketPriority.Title = model.Title;
    }

    #endregion

    #region Client

    public static ClientTicketPriorityViewModel MapToClientTicketPriorityViewModel(this TicketPriority model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
        };  

    #endregion
}
