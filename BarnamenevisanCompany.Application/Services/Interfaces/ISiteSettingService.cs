using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.SiteSetting;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface ISiteSettingService
{
    Task<Result<AdminSiteSettingUpdateViewModel>> FillModelForUpdateAsync(short id);

    Task<Result> UpdateAsync(AdminSiteSettingUpdateViewModel model);

    Task<Result<string>> SiteLogoAsync();

    Task<Result<string>> SiteFavIconAsync();
}