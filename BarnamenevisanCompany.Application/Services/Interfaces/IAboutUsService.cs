using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.AboutUs;
using BarnamenevisanCompany.Domain.ViewModels.Client.AboutUs;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IAboutUsService
{
    Task<Result<AdminUpdateAboutUsViewModel>> FillModelForUpdateAsync(short id);
    
    Task<Result> UpdateAsync(AdminUpdateAboutUsViewModel model);
    
    Task<Result<ClientAboutUsViewModel>> FillModelForShow(short id);

}