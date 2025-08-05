using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.ProjectGalleryMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.ProjectGallery;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class ProjectGalleryService(IProjectGalleryRepository projectGalleryRepository) : IProjectGalleryService
{
    #region FillModelRoGallery

    public async Task<Result<List<AdminProjectGalleryViewModel>>> FillModelForProjectGalleryAsync(short id)
    {
        if (id < 1)
            return Result.Failure<List<AdminProjectGalleryViewModel>>(ErrorMessages.BadRequestError);

        var projectGallerys = await projectGalleryRepository
            .GetAllAsync(x => x.MapToAdminProjectGalleryViewModel(), x => x.ProjectId == id);
        return projectGallerys;
    }

    #endregion

    #region CreateAsync

    public async Task<Result<AdminProjectGalleryViewModel>> CreateAsync(short id, IFormFile image)
    {
        if (id < 1 || image == null)
            return Result.Failure<AdminProjectGalleryViewModel>(ErrorMessages.BadRequestError);

        var imageName = await image.AddImageToServer(FilePaths.ProjectGallery);

        var newGallery = ProjectGalleryMapper.MapToProjectGallery(id, imageName.Value);

        await projectGalleryRepository.InsertAsync(newGallery);

        await projectGalleryRepository.SaveChangesAsync();

        return ProjectGalleryMapper.MapToAdminProjectGalleryViewModel(id, imageName.Value);
    }

    #endregion

    #region DeleteAsync

    public async Task<Result> DeleteAsync(short id, string imageName)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var projectGallery = await projectGalleryRepository.FirstOrDefaultAsync(s => s.ProjectId == id && s.ImageName == imageName);
        if (projectGallery == null)
            return Result.Failure(ErrorMessages.NotFoundError);

        imageName.DeleteImage(FilePaths.ProjectGallery);

        projectGalleryRepository.Delete(projectGallery);

        await projectGalleryRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.DeleteSuccess);
    }

    #endregion
}