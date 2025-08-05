using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.HonorsGalleryMappings;
using BarnamenevisanCompany.Application.Mappers.HonorsMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Honors;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.HonorsGallery;
using BarnamenevisanCompany.Domain.ViewModels.Client.HonorsGallery;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class HonorsGalleryService(IHonorsGalleryRepository honorsGalleryRepository) : IHonorsGalleryService
{
    #region CreateGallery

    public async Task<Result<AdminHonorsGalleryViewModel>> CreatGallery(short id, IFormFile image)
    {
        if (id < 1)
            return Result.Failure<AdminHonorsGalleryViewModel>(ErrorMessages.BadRequestError);

        var imageName = await image.AddImageToServer(FilePaths.HonorsGalleryImage);
        if (imageName.IsFailure)
            return Result.Failure<AdminHonorsGalleryViewModel>(ErrorMessages.SomethingWentWrong);

        var honorGallery = HonorsGalleryMapper.MapToHonorsGallery(id, imageName.Value);


        await honorsGalleryRepository.InsertAsync(honorGallery);
        await honorsGalleryRepository.SaveChangesAsync();

        var adminHonorGallery = HonorsGalleryMapper.MapToAdminHonorsGalleryViewModel(id, imageName.Value);

        return adminHonorGallery;
    }

    #endregion

    public async Task<Result> DeleteFromGallery(short id, string imageName)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var imageGallery = await honorsGalleryRepository.FirstOrDefaultAsync(s => s.HonorsId == id && s.ImageName == imageName);

        if (imageGallery is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        imageName.DeleteImage(FilePaths.HonorsGalleryImage);

        honorsGalleryRepository.Delete(imageGallery);
        await honorsGalleryRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.DeleteSuccess);
    }


    public async Task<Result<List<AdminHonorsGalleryViewModel>>> FillModelForGalleryAsync(short id)
    {
        if (id < 1)
            return Result.Failure<List<AdminHonorsGalleryViewModel>>(ErrorMessages.BadRequestError);
        var gallery = await honorsGalleryRepository.GetAllAsync(u => u.MapToHonorsGallery(), u => u.HonorsId == id);
        if (gallery is null)
            return Result.Failure<List<AdminHonorsGalleryViewModel>>(ErrorMessages.NullValue);
        return gallery;
    }

    public async Task<Result<List<ClientHonorGalleryViewModel>>> GetHonorGalleryAsync(short honorId)
    {
        if (honorId < 1)
            return Result.Failure<List<ClientHonorGalleryViewModel>>(ErrorMessages.BadRequestError);

        var honorGallery = await honorsGalleryRepository
            .GetAllAsync(u => u.MapToClientHonorGalleryViewModel(), u => u.HonorsId == honorId, includes: nameof(HonorsGallery.Honors));

        return honorGallery;
    }
}