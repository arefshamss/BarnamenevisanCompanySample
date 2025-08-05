using BarnamenevisanCompany.Domain.Models.ContactUs;
using BarnamenevisanCompany.Domain.ViewModels.Admin.ContactUsInformation;
using BarnamenevisanCompany.Domain.ViewModels.Client.ContactUsInformation;

namespace BarnamenevisanCompany.Application.Mappers.ContactUsMappings;

public static class ContactUsInformationMapper
{
    
    public static void MapToContactUsInformation(this ContactUsInformation model, AdminUpdateContactUsInformationViewModel contactUsInformation)
    {
        model.Id = contactUsInformation.Id;
        model.Managment = contactUsInformation.Managment;
        model.Address = contactUsInformation.Address;
        model.Email = contactUsInformation.Email;
        model.Latitude = contactUsInformation.Latitude;
        model.Longitude = contactUsInformation.Longitude;
        model.PhoneNumber = contactUsInformation.PhoneNumber;
    }

    public static AdminUpdateContactUsInformationViewModel MapToAdminEditContactUsInformationViewModel(this ContactUsInformation model) =>
        new()
        {
            Id = model.Id,
            Address = model.Address,
            Email = model.Email,
            Latitude = model.Latitude,
            Longitude = model.Longitude,
            Managment = model.Managment,
            PhoneNumber = model.PhoneNumber,
        };

    public static ClientContactUsInformationViewModel MapToContactUsInformationViewModel(this ContactUsInformation model) =>
        new()
        {
            Id = model.Id,
            Address = model.Address,
            Email = model.Email,
            Latitude = model.Latitude,
            Longitude = model.Longitude,
            Managment = model.Managment,
            PhoneNumber = model.PhoneNumber,
        };

}