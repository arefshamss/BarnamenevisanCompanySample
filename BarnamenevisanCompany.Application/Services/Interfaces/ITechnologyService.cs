using BarnamenevisanCompany.Domain.Models.Technology;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Technology;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface ITechnologyService
{
    Task<AdminFilterTechnologyViewModel> FilterAsync(AdminFilterTechnologyViewModel filter);
    
    Task<Result> CreateAsync(AdminCreateTechnologyViewModel model);

    Task<Result> UpdateAsync(AdminUpdateTechnologyViewModel model);

    Task<Result<AdminUpdateTechnologyViewModel>> FillModelForUpdateAsync(short id);
    
    Task<Result> DeleteOrRecoverAsync(short id);
}