using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Faq;
using BarnamenevisanCompany.Domain.ViewModels.Client.Faq;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IFaqService
{
    #region Admin

    Task<Result<AdminFilterFaqViewModel>> FilterAsync(AdminFilterFaqViewModel filter);
    Task<Result> CreateAsync(AdminCreateFaqViewModel model);
    Task<Result<AdminUpdateFaqViewModel>> FillModelForUpdateAsync(short id);
    Task<Result> UpdateAsync(AdminUpdateFaqViewModel model);
    Task<Result> DeleteOrRecoverAsync(short id);

    #endregion

    #region Client

    Task<Result<ClientFilterFaqViewModel>> FilterAsync(ClientFilterFaqViewModel filter);

    #endregion
}