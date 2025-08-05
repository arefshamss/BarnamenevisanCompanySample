using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Honors;
using BarnamenevisanCompany.Domain.ViewModels.Admin.HonorsGallery;
using BarnamenevisanCompany.Domain.ViewModels.Client.Honors;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IHonorsService
{
    Task<AdminFilterHonorsViewModel> FilterAsync(AdminFilterHonorsViewModel filter);
    
    Task<Result> CreateAsync(AdminCreateHonorsViewModel model);

    Task<Result<AdminUpdateHonorsViewModel>> FillModelForUpdateAsync(short id);
    
    Task<Result> UpdateAsync(AdminUpdateHonorsViewModel model);
    
    Task<Result> DeleteOrRecoverAsync(short id);
    
    Task<Result<List<ClientHonorCategoryViewModel>>> GetAllAsync();
    
    
    Task<Result<ClientHonorDetailViewModel>> GetHonorDetailAsync(string slug);
    
    Task<Result> DeleteHonorTeaserAsync(short id);
    

}