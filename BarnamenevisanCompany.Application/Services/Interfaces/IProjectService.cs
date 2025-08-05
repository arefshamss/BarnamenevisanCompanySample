using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Project;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Technology;
using BarnamenevisanCompany.Domain.ViewModels.Client.Project;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IProjectService
{
    Task<AdminFilterProjectViewModel> FilterAsync(AdminFilterProjectViewModel filter);

    Task<Result> CreateAsync(AdminCreateProjectViewModel model);

    Task<Result<AdminUpdateProjectViewModel>> FillModelForUpdateAsync(short id);

    Task<Result> UpdateAsync(AdminUpdateProjectViewModel model);

    Task<Result> DeleteOrRecoverAsync(short id);

    Task<Result<ClientProjectDetailViewModel>> FillModelDetailForClient(short id);

    Task<ClientFilterProjectViewModel> GetAllAsync(ClientFilterProjectViewModel model);
    
    
    Task<Result> DeleteTeaserAsync(short id);
    
}