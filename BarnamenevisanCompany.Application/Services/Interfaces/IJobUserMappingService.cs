using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.JobUserMapping;
using BarnamenevisanCompany.Domain.ViewModels.Client.JobUserMapping;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IJobUserMappingService
{
    #region Admin

    Task<Result<AdminFilterJobUserMappingViewModel>> FilterAsync(AdminFilterJobUserMappingViewModel filter);
    
    Task<Result<AdminJobUserMappingDetailsViewModel>> GetByIdAsync(int id);

    Task<Result> DeleteOrRecoverAsync(int id);

    #endregion

    #region Client

    Task<Result> CreateAsync(ClientCreateJobUserMappingViewModel model);

    Task<Result<ClientFilterJobUserMappingViewModel>> FilterAsync(ClientFilterJobUserMappingViewModel filter);
    
    Task<Result<ClientJobUserMappingDetailsViewModel>> GetByIdAsync(int id, int userId);

    #endregion
}