using BarnamenevisanCompany.Domain.Models.ContactUs;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.ContactUsInformation;
using BarnamenevisanCompany.Domain.ViewModels.Client.ContactUsInformation;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IContactUsInformationService
{
    Task<Result<AdminUpdateContactUsInformationViewModel>> GetContactUsInformationAsync();
    Task<Result> UpdateAsync(AdminUpdateContactUsInformationViewModel model);
    
    Task<Result<ClientContactUsInformationViewModel>> GetSiteContactUsInformationAsync();
    
    
    

}