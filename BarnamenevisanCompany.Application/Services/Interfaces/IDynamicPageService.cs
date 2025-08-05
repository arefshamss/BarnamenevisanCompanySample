using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.DynamicPage;
using BarnamenevisanCompany.Domain.ViewModels.Client.DynamicPage;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IDynamicPageService
{
    #region Admin

    Task<Result<AdminFilterDynamicPageViewModel>> FilterAsync(AdminFilterDynamicPageViewModel filter);

    Task<Result> CreateAsync(AdminCreateDynamicPageViewModel model);

    Task<Result<AdminUpdateDynamicPageViewModel>> FillModelForUpdateAync(short id);

    Task<Result> UpdateAsync(AdminUpdateDynamicPageViewModel model);

    Task<Result> DeleteOrRecoverAsync(short id);

    #endregion

    #region Client

    Task<Result<ClientDynamicPageViewModel>> GetBySlugAsync(string slug);

    #endregion
}