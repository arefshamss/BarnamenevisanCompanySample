using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Ticket;
using BarnamenevisanCompany.Domain.ViewModels.Client.Ticket;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface ITicketService
{
    #region Admin

    Task<Result<AdminFilterTicketsViewModel>> FilterAsync(AdminFilterTicketsViewModel filter);
    Task<Result<AdminTicketViewModel>> GetByIdAsync(int id);
    Task<Result<AdminUpdateTicketViewModel>> FillModelForUpdateAsync(int id);
    Task<Result> ToggleCloseTicketAsync(int id, int userId);    
    Task CloseOldTicketsAsync();    

    Task<Result<int>> CreateAsync(AdminCreateTicketViewModel model);
    Task<Result> UpdateAsync(AdminUpdateTicketViewModel model);
    Task<Result> DeleteOrRecoverAsync(int id);
    
    #endregion

    #region Client

    Task<Result<int>> CreateAsync(ClientCreateTicketViewModel model);   
    Task<Result<ClientFilterTicketsViewModel>> FilterAsync(ClientFilterTicketsViewModel filter);
    Task<Result<ClientTicketViewModel>> GetByIdForUserPanelAsync(int id);   

    #endregion
}