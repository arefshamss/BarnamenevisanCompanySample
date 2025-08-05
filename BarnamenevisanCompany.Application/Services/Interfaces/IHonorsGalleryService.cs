using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.HonorsGallery;
using BarnamenevisanCompany.Domain.ViewModels.Client.HonorsGallery;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IHonorsGalleryService
{
    Task<Result<AdminHonorsGalleryViewModel>> CreatGallery(short id,IFormFile image);
    
    Task<Result> DeleteFromGallery(short id,string imageName);
    
    Task<Result<List<AdminHonorsGalleryViewModel>>> FillModelForGalleryAsync(short id);
    
    Task<Result<List<ClientHonorGalleryViewModel>>> GetHonorGalleryAsync(short honorId);
}