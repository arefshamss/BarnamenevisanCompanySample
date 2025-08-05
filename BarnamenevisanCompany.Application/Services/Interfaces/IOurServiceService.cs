using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OurService;
using BarnamenevisanCompany.Domain.ViewModels.Client.OurService;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IOurServiceService
{
    Task<AdminFilterOurServiceViewModel> FilterAsync(AdminFilterOurServiceViewModel filter);
    Task<Result> CreateAsync(AdminCreateOurServiceViewModel model);
    
    Task<Result> UpdateAsync(AdminUpdateOurServiceViewModel model);
    
    Task<Result<AdminUpdateOurServiceViewModel>> FillModelForUpdateAsync(short id);

    Task<Result> DeleteOrRecoverAsync(short id);
    
    Task<Result<ClientFilterServiceViewModel>> GetAllServiceAsync(ClientFilterServiceViewModel model);
    
    Task<Result<ClientDetailServiceViewModel>> FillModelForShow(string slug);
    
    
    
}