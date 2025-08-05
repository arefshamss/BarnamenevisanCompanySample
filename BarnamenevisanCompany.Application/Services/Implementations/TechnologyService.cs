using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.TechnologyMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Models.Order;
using BarnamenevisanCompany.Domain.Models.Technology;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Technology;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class TechnologyService(
    ITechnologyRepository technologyRepository,
    IProjectTechnologyMappingRepository projectTechnologyMappingRepository) : ITechnologyService
{
    #region FilterAsync

    public async Task<AdminFilterTechnologyViewModel> FilterAsync(AdminFilterTechnologyViewModel filter)
    {
        filter = filter ?? new AdminFilterTechnologyViewModel();
        var condition = Filter.GenerateConditions<Technology>();
        var order = Filter.GenerateOrders<Technology>();

        #region Filter

        if (!string.IsNullOrEmpty(filter.Title))
            condition.Add(u => EF.Functions.Like(u.Title, $"%{filter.Title}%"));

        switch (filter.DeleteStatus)
        {
            case DeleteStatus.All:
                break;
            case DeleteStatus.Deleted:
                condition.Add(u => u.IsDeleted);
                break;
            case DeleteStatus.NotDeleted:
                condition.Add(u => !u.IsDeleted);
                break;
        }

        switch (filter.FilterOrderBy)
        {
            case FilterOrderBy.Ascending:
                order.Add(u => u.CreatedDate);
                break;
            case FilterOrderBy.Descending:
                break;
        }

        #endregion

        await technologyRepository.FilterAsync(filter, condition, s => s.MapToAdminTechnologyViewModel(), order);
        return filter;
    }

    #endregion

    #region CreateAsync

    public async Task<Result> CreateAsync(AdminCreateTechnologyViewModel model)
    {
        if (model.Image is null)
            return Result.Failure(ErrorMessages.ImageIsRequired);

        if (!model.Image.IsSvg())
            return Result.Failure(ErrorMessages.FileFormatError);

        var imageName = await model.Image.AddImageToServer(FilePaths.TechnologyImage, checkImageFormat: false);
        model.ImageName = imageName.Value;

        var technology = model.MapToTechnology();

        await technologyRepository.InsertAsync(technology);
        await technologyRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region Update

    public async Task<Result> UpdateAsync(AdminUpdateTechnologyViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var technology = await technologyRepository.GetByIdAsync(model.Id);

        if (technology is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        if (model.Image is not null && model.Image.IsSvg())
        {
            model.ImageName.DeleteImage(FilePaths.TechnologyImage);
            var imageName = await model.Image.AddImageToServer(FilePaths.TechnologyImage, checkImageFormat: false);
            model.ImageName = imageName.Value;
        }

        technology.MaptoTechnology(model);

        technologyRepository.Update(technology);
        await technologyRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region FillModelForUpdate

    public async Task<Result<AdminUpdateTechnologyViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateTechnologyViewModel>(ErrorMessages.BadRequestError);

        var technology = await technologyRepository.GetByIdAsync(id);

        if (technology is null)
            return Result.Failure<AdminUpdateTechnologyViewModel>(ErrorMessages.NotFoundError);

        return technology.MapToAdminUpdateTechnologyViewModel();
    }

    #endregion

    #region DeleteOrRecoverAsync

    public async Task<Result> DeleteOrRecoverAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var technology = await technologyRepository.GetByIdAsync(id);

        if (technology is null)
            return Result.Failure(ErrorMessages.NotFoundError);
        var result = technologyRepository.SoftDeleteOrRecover(technology);

        await technologyRepository.SaveChangesAsync();
        if (result)
        {
            await projectTechnologyMappingRepository.ExecuteDeleteRange(s => s.TechnologyId == id);
            return Result.Success(SuccessMessages.DeleteSuccess);
        }


        return Result.Success(SuccessMessages.RecoverSuccess);
    }

    #endregion
}   