using BarnamenevisanCompany.Application.Mappers.DynamicPageMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.DynamicPage;
using BarnamenevisanCompany.Domain.Models.DynamicPage;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.DynamicPage;
using BarnamenevisanCompany.Domain.ViewModels.Client.DynamicPage;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class DynamicPageService(
    IDynamicPageRepository dynamicPageRepository
) : IDynamicPageService
{
    #region Admin

    #region FilterAsync

    public async Task<Result<AdminFilterDynamicPageViewModel>> FilterAsync(AdminFilterDynamicPageViewModel filter)
    {
        filter??=new();

        var conditions = Filter.GenerateConditions<DynamicPage>();
        var orders = Filter.GenerateOrders<DynamicPage>();

        #region Filter

        if (!string.IsNullOrEmpty(filter.Title))
            conditions.Add(x => EF.Functions.Like(x.Title, $"%{filter.Title}%"));

        switch (filter.DeleteStatus)
        {
            case DeleteStatus.NotDeleted:
                conditions.Add(x => !x.IsDeleted);
                break;
            case DeleteStatus.All:
                break;
            case DeleteStatus.Deleted:
                conditions.Add(x => x.IsDeleted);
                break;
        }

        switch (filter.FilterOrderBy)
        {
            case FilterOrderBy.Descending:
                orders.Add(x => x.CreatedDate, FilterOrderBy.Descending);
                break;
            case FilterOrderBy.Ascending:
                orders.Add(x => x.CreatedDate);
                break;
        }

        switch (filter.ActiveStatus)
        {
            case DynamicPageActiveStatus.Active:
                conditions.Add(x => x.IsActive);
                break;
            case DynamicPageActiveStatus.All:
                break;
            case DynamicPageActiveStatus.NotActive:
                conditions.Add(x => !x.IsActive);
                break;
        }

        #endregion

        await dynamicPageRepository.FilterAsync(filter, conditions, x => x.MapToAdminDynamicPageViewModel(), orders);
        return filter;
    }

    #endregion

    #region CreateAsync

    public async Task<Result> CreateAsync(AdminCreateDynamicPageViewModel model)
    {
        #region Validations

        if (await dynamicPageRepository.AnyAsync(x => x.Slug == model.Slug))
            return Result.Failure(string.Format(ErrorMessages.AlreadyExistError, "URL"));

        #endregion
        
        var dynamicPage = model.MapToDynamicPage();

        await dynamicPageRepository.InsertAsync(dynamicPage);
        await dynamicPageRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region FillModelForUpdateAsync

    public async Task<Result<AdminUpdateDynamicPageViewModel>> FillModelForUpdateAync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateDynamicPageViewModel>(ErrorMessages.BadRequestError);

        var dynamicPage = await dynamicPageRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (dynamicPage is null)
            return Result.Failure<AdminUpdateDynamicPageViewModel>(ErrorMessages.NotFoundError);

        return dynamicPage.MapToAdminUpdateDynamicPageViewModel();
    }

    #endregion

    #region UpdateAsync

    public async Task<Result> UpdateAsync(AdminUpdateDynamicPageViewModel model)
    {
        #region Validations

        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        if (await dynamicPageRepository.AnyAsync(x => x.Slug == model.Slug && x.Id != model.Id))
            return Result.Failure(string.Format(ErrorMessages.AlreadyExistError, "URL"));

        #endregion

        var dynamicPage = await dynamicPageRepository.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);

        if (dynamicPage is null)
            return Result.Failure<Result>(ErrorMessages.NotFoundError);

        dynamicPage.UpdateDynamicPage(model);

        dynamicPageRepository.Update(dynamicPage);
        await dynamicPageRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region DeleteOrRecoverAsync

    public async Task<Result> DeleteOrRecoverAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.SomethingWentWrong);

        var dynamicPage = await dynamicPageRepository.GetByIdAsync(id);

        if (dynamicPage is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        var result = dynamicPageRepository.SoftDeleteOrRecover(dynamicPage);
        await dynamicPageRepository.SaveChangesAsync();

        return result ? Result.Success(SuccessMessages.DeleteSuccess) : Result.Success(SuccessMessages.RecoverSuccess);
    }

#endregion

    #endregion

    #region Client

    #region GetBySlugAsync

    public async Task<Result<ClientDynamicPageViewModel>> GetBySlugAsync(string slug)
    {
        if (string.IsNullOrEmpty(slug))
            return Result.Failure<ClientDynamicPageViewModel>(ErrorMessages.BadRequestError);

        var result = await dynamicPageRepository.FirstOrDefaultAsync(x => x.Slug == slug && !x.IsDeleted && x.IsActive);

        if (result is null)
            return Result.Failure<ClientDynamicPageViewModel>(ErrorMessages.NotFoundError);

        return result.MapToClientDynamicPageViewModel();
    }

    #endregion

    #endregion
}