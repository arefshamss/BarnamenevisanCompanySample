using System.Diagnostics;
using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.OurServiceMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Models.OurServices;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OurService;
using BarnamenevisanCompany.Domain.ViewModels.Client.OurService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class OurServiceService(IOurServiceRepository ourServiceRepository) : IOurServiceService
{
    #region Admin

    #region Filter

    public async Task<AdminFilterOurServiceViewModel> FilterAsync(AdminFilterOurServiceViewModel filter)
    {
        var condition = Filter.GenerateConditions<OurService>();
        var order = Filter.GenerateOrders<OurService>();

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
                order.Add(u => u.CreatedDate, FilterOrderBy.Ascending);
                break;
            case FilterOrderBy.Descending:
                break;
        }

        #endregion

        await ourServiceRepository.FilterAsync(filter, condition, u => u.MapTOurService(), order);
        return filter;
    }

    #endregion

    #region Create

    public async Task<Result> CreateAsync(AdminCreateOurServiceViewModel model)
    {
        if (model.Image == null)
            return Result.Failure(ErrorMessages.ImageIsRequired);

        if (model.Image.IsImage())
            return Result.Failure(ErrorMessages.FileFormatError);

        if (await ourServiceRepository.AnyAsync(s => s.Title.ToLower().Trim() == model.Title.ToLower().Trim()))
            return Result.Failure(string.Format(ErrorMessages.AlreadyExistError, model.Title));

        if (model.Image.IsSvg())
        {
            var result = await model.Image.AddImageToServer(FilePaths.OurServiceOriginalImage, checkImageFormat: false);
            model.ImageName = result.Value;
        }

        var newService = model.MapTOurService();

        await ourServiceRepository.InsertAsync(newService);
        await ourServiceRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region Update

    public async Task<Result> UpdateAsync(AdminUpdateOurServiceViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);
        if (model.ImageName is null)
            return Result.Failure(ErrorMessages.ImageIsRequired);


        var service = await ourServiceRepository.GetByIdAsync(model.Id);
        if (service is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        if (model.Title.ToLower().Trim() != service.Title.ToLower().Trim() && await ourServiceRepository.AnyAsync(s => s.Title.ToLower().Trim() == model.Title.ToLower().Trim()))
            return Result.Failure(ErrorMessages.AlreadyExistError);


        if (model.Image != null)
        {
            if (model.Image.IsImage())
                return Result.Failure(ErrorMessages.FileFormatError);

            if (model.Image.IsSvg())
            {
                var result = await model.Image.AddImageToServer
                    (FilePaths.OurServiceOriginalImage, deleteFileName: service.ImageName, checkImageFormat: false);
                model.ImageName = result.Value;
            }
        }


        service.MapTOurService(model);
        ourServiceRepository.Update(service);

        await ourServiceRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region FillModelUpdate

    public async Task<Result<AdminUpdateOurServiceViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateOurServiceViewModel>(ErrorMessages.BadRequestError);

        var ourService = await ourServiceRepository.GetByIdAsync(id);
        if (ourService is null)
            return Result.Failure<AdminUpdateOurServiceViewModel>(ErrorMessages.NotFoundError);

        return ourService.MapToAdminUpdateOurServiceViewModel();
    }

    #endregion

    #region DeleteOrRecoverAsync

    public async Task<Result> DeleteOrRecoverAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var service = await ourServiceRepository.GetByIdAsync(id);

        if (service is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        var result = ourServiceRepository.SoftDeleteOrRecover(service);

        await ourServiceRepository.SaveChangesAsync();

        return result ? Result.Success(SuccessMessages.DeleteSuccess) : Result.Success(SuccessMessages.RecoverSuccess);
    }

    #endregion

    #endregion

    #region Client

    public async Task<Result<ClientFilterServiceViewModel>> GetAllServiceAsync(ClientFilterServiceViewModel model)
    {
        var condition = Filter.GenerateConditions<OurService>();
        condition.Add(s => !s.IsDeleted);

        await ourServiceRepository.FilterAsync(model, condition, u => u.MapToClientGetAllServiceViewModel());
        return model;
    }

    [HttpGet("Service{slug}")]
    public async Task<Result<ClientDetailServiceViewModel>> FillModelForShow(string slug)
    {
        if (slug is null)
            return Result.Failure<ClientDetailServiceViewModel>(ErrorMessages.BadRequestError);

        var service = await ourServiceRepository.FirstOrDefaultAsync(u => u.Slug == slug);

        if (service is null)
            return Result.Failure<ClientDetailServiceViewModel>(ErrorMessages.NotFoundError);

        return service.MapToClientDetailServiceViewModel();
    }

    #endregion
}