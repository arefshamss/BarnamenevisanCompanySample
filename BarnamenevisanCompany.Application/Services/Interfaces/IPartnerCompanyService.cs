using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.PartnerCompany;
using BarnamenevisanCompany.Domain.ViewModels.Client.PartnerCompany;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IPartnerCompanyService
{
    #region Admin

    Task<Result<AdminFilterPartnerCompanyViewModel>> FilterAsync(AdminFilterPartnerCompanyViewModel filter);

    Task<Result> CreateAsync(AdminCreatePartnerCompanyViewModel model);

    Task<Result<AdminUpdatePartnerCompanyViewModel>> FillModelForUpdateAsync(short id);

    Task<Result> UpdateAsync(AdminUpdatePartnerCompanyViewModel model);

    Task<Result> DeleteOrRecoverAsync(short id);

    #endregion

    #region Client

    Task<Result<ClientFilterPartnerCompanyViewModel>> FilterAsync(ClientFilterPartnerCompanyViewModel filter);

    #endregion
}