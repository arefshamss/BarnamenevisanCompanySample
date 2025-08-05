using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.ProjectGallery;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IProjectGalleryService
{
    Task<Result<List<AdminProjectGalleryViewModel>>> FillModelForProjectGalleryAsync(short id);
    
    Task<Result<AdminProjectGalleryViewModel>> CreateAsync(short id,IFormFile image); 
    
    Task<Result> DeleteAsync(short id,string imageName);
}