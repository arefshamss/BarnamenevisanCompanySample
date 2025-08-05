using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.TicketMessage;
using BarnamenevisanCompany.Domain.ViewModels.Client.TicketMessage;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface ITicketMessageService
{
    #region Admin

    Task<Result> CreateAsync(AdminCreateTicketMessageViewModel model);

    #endregion

    #region Client

    Task<Result> CreateAsync(ClientCreateTicketMessageViewModel model);

    #endregion
}