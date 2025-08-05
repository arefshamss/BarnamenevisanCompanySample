using BarnamenevisanCompany.Application.Mappers.ContactUsMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.ContactUsInformation;
using BarnamenevisanCompany.Domain.ViewModels.Client.ContactUsInformation;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class ContactUsInformationService(IContactUsInformationRepository contactUsInformationRepository) : IContactUsInformationService
{
    #region Admin

    public async Task<Result<AdminUpdateContactUsInformationViewModel>> GetContactUsInformationAsync()
    {
        var contactUsInformation = await contactUsInformationRepository.GetByIdAsync(1);

        if (contactUsInformation is null)
            return Result.Failure<AdminUpdateContactUsInformationViewModel>(ErrorMessages.NotFoundError);

        return contactUsInformation.MapToAdminEditContactUsInformationViewModel();
    }

    public async Task<Result> UpdateAsync(AdminUpdateContactUsInformationViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var contactUsInformation = await contactUsInformationRepository.GetByIdAsync(model.Id);
        if (contactUsInformation is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        contactUsInformation.MapToContactUsInformation(model);

        contactUsInformationRepository.Update(contactUsInformation);
        await contactUsInformationRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    public async Task<Result<ClientContactUsInformationViewModel>> GetSiteContactUsInformationAsync()
    {
        var information = await contactUsInformationRepository.GetByIdAsync(1);
        if (information is null)
            return Result.Failure<ClientContactUsInformationViewModel>(ErrorMessages.NotFoundError);

        return information.MapToContactUsInformationViewModel();
    }
}