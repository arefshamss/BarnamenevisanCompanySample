using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Job;
using BarnamenevisanCompany.Domain.ViewModels.Client.Job;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IJobService
{
    #region Admin
    Task<Result<AdminFilterJobViewModel>> FilterAsync(AdminFilterJobViewModel filter);

    Task<Result> CreateAsync(AdminCreateJobViewModel model);
    
    Task<Result<AdminUpdateJobViewModel>> FillModelForUpdateAsync(int id);
    
    Task<Result> UpdateAsync(AdminUpdateJobViewModel model);
    
    Task<Result> DeleteOrRecoverAsync(int id);

    #endregion

    #region Client

    Task<Result<ClientFilterJobViewModel>> FilterAsync(ClientFilterJobViewModel filter);
    
    Task<Result<ClientJobDetailsViewModel>> GetBySlugAsync(string slug);

    Task<Result<string>> GetSlugByIdAsync(int id);

    Task<bool> IsActiveJobExistsAsync();  
    
    #endregion
}