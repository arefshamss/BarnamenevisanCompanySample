using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.HonorsGalleryMappings;
using BarnamenevisanCompany.Application.Mappers.HonorsMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Models.Honors;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Honors;
using BarnamenevisanCompany.Domain.ViewModels.Client.Honors;
using Microsoft.EntityFrameworkCore;


namespace BarnamenevisanCompany.Application.Services.Implementations;

public class HonorsService(
    IHonorsRepository honorsRepository,
    IHonorsGalleryRepository honorsGalleryRepository) : IHonorsService
{
    #region FilterAsync

    public async Task<AdminFilterHonorsViewModel> FilterAsync(AdminFilterHonorsViewModel filter)
    {
        var condition = Filter.GenerateConditions<Honors>();
        var orders = Filter.GenerateOrders<Honors>();

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

        switch (filter.OrderBy)
        {
            case FilterOrderBy.Ascending:
                break;
            case FilterOrderBy.Descending:
                orders.Add(u => u.CreatedDate);
                break;
        }
        

        #endregion


        await honorsRepository.FilterAsync(filter, condition, u => u.MapToAdminHonorsViewModel(), orders);

        return filter;
    }

    #endregion

    #region Create

    public async Task<Result> CreateAsync(AdminCreateHonorsViewModel model)
    {
        var exist = await honorsRepository.AnyAsync(u => u.Slug.ToLower().Trim() == model.Slug.ToLower().Trim() && u.CompanyId == model.CompanyId);
        if (exist)
            return Result.Failure(string.Format(ErrorMessages.AlreadyExistError, model.Title));
        {
            var imageName = await model.Image.AddImageToServer(FilePaths.HonorsImage);

            if (model.TeaserPoster != null)
            {
                var result = await model.TeaserPoster.AddImageToServer(FilePaths.HonorTeaserPosterImage);
                model.TeaserPosterName = result.Value;
            }

            model.ImageName = imageName.Value;

            var newHonors = model.MapToHonors();

            await honorsRepository.InsertAsync(newHonors);
            await honorsRepository.SaveChangesAsync();

            return Result.Success(SuccessMessages.InsertSuccessfullyDone);
        }
    }

    #endregion

    #region FillModelForUpdate

    public async Task<Result<AdminUpdateHonorsViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateHonorsViewModel>(ErrorMessages.BadRequestError);
        var honorsGalley = await honorsGalleryRepository
            .GetAllAsync(u => u.MapToHonorsGallery(), u => u.HonorsId == id);

        var honor = await honorsRepository.GetByIdAsync(id);

        if (honor is null)
            return Result.Failure<AdminUpdateHonorsViewModel>(ErrorMessages.NotFoundError);

        var model = honor.MapToAdminCreateHonorsViewModel();
        return model;
    }

    #endregion

    #region Update

    public async Task<Result> UpdateAsync(AdminUpdateHonorsViewModel model)
    {
        if (model.HonorsId < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var honors = await honorsRepository.GetByIdAsync(model.HonorsId);

        if (honors is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        if (model.CompanyId != honors.CompanyId || model.Title.ToLower().Trim() != honors.Title.ToLower().Trim())
        {
            var exist = await honorsRepository.AnyAsync(u => u.Title.ToLower().Trim() == model.Title.ToLower().Trim() && u.CompanyId == model.CompanyId);
            if (exist)
                return Result.Failure(string.Format(ErrorMessages.AlreadyExistError, model.Slug));
        }


        if (model.Image is not null)
        {
            var result = await model.Image.AddImageToServer(FilePaths.HonorsImage, deleteFileName: honors.ImageName);
            if (result.IsFailure)
                return Result.Failure(result.Message);
            model.ImageName = result.Value;
        }

        if (model.TeaserPoster != null)
        {
            if (model.TeaserPosterImageName is not null)
            {
                model.TeaserPosterImageName.DeleteImage(FilePaths.HonorTeaserPosterImage);
            }

            var result = await model.TeaserPoster.AddImageToServer(FilePaths.HonorTeaserPosterImage);
            model.TeaserPosterImageName = result.Value;
        }

        if (honors.TeaserName != null && model.TeaserName != null)
        {
            if (model.TeaserName != honors.TeaserName)
            {
                honors.TeaserName.DeleteFile(FilePaths.HonorsTeaser);
            }
        }


        honors.MapToHonors(model);
        honorsRepository.Update(honors);
        await honorsRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region DeleteOrRecover

    public async Task<Result> DeleteOrRecoverAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var honors = await honorsRepository.GetByIdAsync(id);

        if (honors is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        honors.TeaserName?.DeleteFile(FilePaths.HonorsTeaser);

        honors.TeaserPoster?.DeleteFile(FilePaths.HonorTeaserPosterImage);

        #region DeleteGallery

        var honorsGallery = await honorsGalleryRepository.GetAllAsync(u => u.HonorsId == id);

        if (honorsGallery is not null)
        {
            foreach (var item in honorsGallery)
            {
                item.ImageName.DeleteImage(FilePaths.HonorsGalleryImage);
                honorsGalleryRepository.Delete(item);
            }

            await honorsGalleryRepository.SaveChangesAsync();
        }

        #endregion

        var result = honorsRepository.SoftDeleteOrRecover(honors);
        await honorsRepository.SaveChangesAsync();

        return result ? Result.Success(SuccessMessages.DeleteSuccess) : Result.Success(SuccessMessages.RecoverSuccess);
    }

    #endregion

    #region GetAllAsync

    public async Task<Result<List<ClientHonorCategoryViewModel>>> GetAllAsync()
    {
        var honors = await honorsRepository.GetAllAsync(u => !u.IsDeleted, includes: nameof(Honors.Company));
        return honors.GroupBy(u => u.Company.Title).Select(x => new ClientHonorCategoryViewModel()
        {
            CompanyName = x.Key,
            Honors = x.Select(u => u.MapToClientHonorsViewModel()).ToList()
        }).ToList();
    }

    #endregion

    #region HonorDetail

    public async Task<Result<ClientHonorDetailViewModel>> GetHonorDetailAsync(string slug)
    {
        if (slug is null)
            return Result.Failure<ClientHonorDetailViewModel>(ErrorMessages.BadRequestError);


        var honors = await honorsRepository.FirstOrDefaultAsync(u => u.Slug == slug, includes: [nameof(Honors.Company), nameof(Honors.HonorsGalleries)]);

        if (honors is null)
            return Result.Failure<ClientHonorDetailViewModel>(ErrorMessages.NotFoundError);


        return honors.MapToClientHonorDetailViewModel();
    }

    #endregion

    #region DeleteHonorTeaserAsync

    public async Task<Result> DeleteHonorTeaserAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var honor = await honorsRepository.GetByIdAsync(id);

        if (honor is null)
            return Result.Failure(ErrorMessages.NullValue);

        honor.TeaserName.DeleteFile(FilePaths.HonorsTeaserChunk);

        honor.TeaserName = null;

        honorsRepository.Update(honor);

        await honorsRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.DeleteSuccess);
    }

    #endregion
}