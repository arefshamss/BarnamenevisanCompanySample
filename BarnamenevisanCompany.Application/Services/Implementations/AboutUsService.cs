using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.AboutUsMappings;
using BarnamenevisanCompany.Application.Mappers.OrderMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.AboutUs;
using BarnamenevisanCompany.Domain.ViewModels.Client.AboutUs;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class AboutUsService(
    IAboutUsRepository aboutUsRepository,
    ISiteSettingRepository siteSettingRepository,
    IOrderStepRepository orderStepRepository) : IAboutUsService
{
    public async Task<Result<AdminUpdateAboutUsViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateAboutUsViewModel>(ErrorMessages.BadRequestError);
        var aboutUsUpdate = await aboutUsRepository.GetByIdAsync(id);

        if (aboutUsUpdate is null)
            return Result.Failure<AdminUpdateAboutUsViewModel>(ErrorMessages.NullValue);

        return aboutUsUpdate.MapToAdminAboutUsUpdateAsync();
    }


    public async Task<Result> UpdateAsync(AdminUpdateAboutUsViewModel model)
    {
        var aboutUsUpdate = await aboutUsRepository.GetByIdAsync(1);

        if (aboutUsUpdate is null)
            return Result.Failure(ErrorMessages.NullValue);

        aboutUsUpdate.MapToAboutUs(model);

        aboutUsRepository.Update(aboutUsUpdate);

        await aboutUsRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    public async Task<Result<ClientAboutUsViewModel>> FillModelForShow(short id)
    {
        if (id < 1)
            return Result.Failure<ClientAboutUsViewModel>(ErrorMessages.BadRequestError);

        var aboutUsShow = await aboutUsRepository.GetByIdAsync(id);

        if (aboutUsShow is null)
            return Result.Failure<ClientAboutUsViewModel>(ErrorMessages.NullValue);

        var siteSetting = await siteSettingRepository.GetByIdAsync(1);
        if (siteSetting is null)
            return Result.Failure<ClientAboutUsViewModel>(ErrorMessages.NullValue);
        var orderSteps = await orderStepRepository.GetAllAsync(s => s.MapToClientOrderStepViewModel(), s => !s.IsDeleted);
        return aboutUsShow.MapToClientAboutUsViewModel(orderSteps, siteSetting.IndexOrderTitle, siteSetting.IndexOrderParaghraph);
    }
}