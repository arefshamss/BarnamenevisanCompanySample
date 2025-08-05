using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OrderStep;
using BarnamenevisanCompany.Domain.ViewModels.Client.OrderStep;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IOrderStepService
{
    Task<AdminFilterOrderStepViewModel> FilterAsync(AdminFilterOrderStepViewModel filter);
    Task<Result> CrateAsync(AdminCreateOrderStepViewModel model);
    Task<Result> UpdateAsync(AdminUpdateOrderStepViewModel model);

    Task<Result<AdminUpdateOrderStepViewModel>> FillModelForUpdateAsync(short orderStepId);

    Task<Result> DeleteOrRecoverAsync(short orderStepId);

    Task<Result<List<ClientOrderStepViewModel>>> GetClientOrderStepAsync();
}