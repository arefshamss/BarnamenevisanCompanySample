using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Company;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface ICompanyService
{
    #region Admin

    Task<Result<AdminFilterCompanyViewModel>> FilterAsync(AdminFilterCompanyViewModel filter);
    Task<Result> CreateAsync(AdminCreateCompanyViewModel model);
    Task<Result<AdminUpdateCompanyViewModel>> FillModelForUpdateAsync(short id);
    Task<Result> UpdateAsync(AdminUpdateCompanyViewModel model);
    Task<Result> DeleteOrRecoverAsync(short id);
    Task<SelectList> AdminGetSelectListAsync();

    #endregion
}