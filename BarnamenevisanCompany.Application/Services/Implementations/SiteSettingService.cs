using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.SiteSettingMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.SiteSetting;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class SiteSettingService(ISiteSettingRepository siteSettingRepository) : ISiteSettingService
{
    #region FillModelForUpdateASync

    public async Task<Result<AdminSiteSettingUpdateViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminSiteSettingUpdateViewModel>(ErrorMessages.BadRequestError);

        var siteSetting = await siteSettingRepository.GetByIdAsync(1);

        if (siteSetting is null)
            return Result.Failure<AdminSiteSettingUpdateViewModel>(ErrorMessages.NullValue);

        return siteSetting.MapAdminSiteSettingUpdateViewModel();
    }

    #endregion

    #region UpdateAsync

    public async Task<Result> UpdateAsync(AdminSiteSettingUpdateViewModel model)
    {
        var siteSetting = await siteSettingRepository.GetByIdAsync(1);

        if (siteSetting is null)
            return Result.Failure(ErrorMessages.NullValue);
        if (model.SiteLogo is not null)
        {
            var imagName = await model.SiteLogo.AddImageToServer(FilePaths.SiteSettings, checkImageFormat: false, deleteFileName: siteSetting.SiteLogo);
            model.SiteLogoImageName = imagName.Value;
        }

        if (model.SiteFavIcon is not null)
        {
            if (!model.SiteFavIcon.IsIcon())
                return Result.Failure(ErrorMessages.FileFormatError);
            var favIconName = await model.SiteFavIcon.AddImageToServer(FilePaths.SiteSettings, checkImageFormat: false, deleteFileName: siteSetting.FavIcon);
            model.SiteFavIconImageName = favIconName.Value;
        }

        if (model.JobListImage is not null)
        {
            if (!model.JobListImage.IsImage())
                return Result.Failure(ErrorMessages.FileFormatError);
            var jonListImageName = await model.JobListImage.AddImageToServer(FilePaths.SiteSettings, checkImageFormat: false, deleteFileName: siteSetting.FavIcon);
            model.JobListImageName = jonListImageName.Value;
        }
        
        siteSetting.MapAdminSiteSetting(model);

        siteSettingRepository.Update(siteSetting);
        await siteSettingRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region SiteLogo

    public async Task<Result<string>> SiteLogoAsync()
    {
        var siteSetting = await siteSettingRepository.GetByIdAsync(1);

        if (siteSetting is null)
            return Result.Failure<string>(ErrorMessages.NullValue);

        return siteSetting.SiteLogo;
    }

    public async Task<Result<string>> SiteFavIconAsync()
    {
        var siteSetting = await siteSettingRepository.GetByIdAsync(1);

        if (siteSetting is null)
            return Result.Failure<string>(ErrorMessages.NullValue);

        return siteSetting.FavIcon;
    }

    #endregion
}