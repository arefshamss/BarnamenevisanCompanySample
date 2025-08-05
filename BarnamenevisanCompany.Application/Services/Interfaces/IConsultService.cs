using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Consult;
using BarnamenevisanCompany.Domain.ViewModels.Client;
using BarnamenevisanCompany.Domain.ViewModels.Client.Consult;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IConsultService
{
    Task<AdminFilterConsultViewModel> FilterAsync(AdminFilterConsultViewModel filter);

    Task<Result> DeleteOrRecoverAsync(short id);

    Task<Result<AdminConsultDetailViewModel>> FillModelDetailForShow(short id);

    Task<Result> AnswerAsync(AdminAnswerConsultViewModel model);

    Task<Result> SendSmsForAnswerAsync(string phoneNumber, string message);

    Task<Result> CreateAsync(ClientCreateConsultViewModel model);

    Task<Result<string>> GetPageInformation(byte id);

    Task<Result<ClientConsultFilterViewModel>> FilterAsync(ClientConsultFilterViewModel filter, int userId);

    Task<Result<ClientConsultDetailViewModel>> GetConsultDetailAsync(short id);
}