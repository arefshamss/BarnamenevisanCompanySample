using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.SocialNetworkMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Models.Order;
using BarnamenevisanCompany.Domain.Models.SocialNetwork;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.SocialNetwork;
using BarnamenevisanCompany.Domain.ViewModels.Client.SocialNetwork;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class SocialNetworkService(ISocialNetworkRepository socialNetworkRepository) : ISocialNetworkService
{
    #region Filter

    public async Task<AdminFilterSocialNetworkViewModel> FilterAsync(AdminFilterSocialNetworkViewModel filter)
    {
        filter = filter ?? new AdminFilterSocialNetworkViewModel();

        var condition = Filter.GenerateConditions<SocialNetwork>();
        var order = Filter.GenerateOrders<SocialNetwork>();

        #region Filter

        if (!string.IsNullOrEmpty(filter.Title))
            condition.Add(u => EF.Functions.Like(u.Title, $"%{filter.Title}%"));


        switch (filter.FilterOrderBy)
        {
            case FilterOrderBy.Ascending:
                order.Add(u => u.CreatedDate);
                break;
            case FilterOrderBy.Descending:
                break;
        }

        #endregion

        await socialNetworkRepository.FilterAsync(filter, condition, u => u.MapToSocialNetworkViewModel(), order);
        return filter;
    }

    #endregion

    #region Create

    public async Task<Result> CreateAsync(AdminCreateSocialNetworkViewModel model)
    {
        if (await socialNetworkRepository.AnyAsync(u => u.Title.ToLower().Trim() == model.Title.ToLower().Trim()))
        {
            return Result.Failure(string.Format(ErrorMessages.AlreadyExistError, model.Title));
        }

        var res = model.Icon.IsSvg();
        if (!res)
            return Result.Failure(ErrorMessages.FileFormatError);

        var iconName = await model.Icon.AddImageToServer(FilePaths.SocialNetworkImage, checkImageFormat: false);
        model.IconName = iconName.Value;

        await socialNetworkRepository.InsertAsync(model.MapToSocialNetwork());

        await socialNetworkRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region FillModelForUpdate

    public async Task<Result<AdminSocialNetworkUpdateViewModel>> FillModelForUpdateAsync(byte id)
    {
        if (id < 1)
            return Result.Failure<AdminSocialNetworkUpdateViewModel>(ErrorMessages.BadRequestError);

        var socialNetwork = await socialNetworkRepository.GetByIdAsync(id);

        if (socialNetwork is null)
            return Result.Failure<AdminSocialNetworkUpdateViewModel>(ErrorMessages.NotFoundError);

        return socialNetwork.MapToAdminSocialNetworkUpdateViewModel();
    }

    #endregion

    #region UpdateAsync

    public async Task<Result> UpdateAsync(AdminSocialNetworkUpdateViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        
        var socialNetwork = await socialNetworkRepository.GetByIdAsync(model.Id);
        
        if (socialNetwork is null)
            return Result.Failure(ErrorMessages.NotFoundError);
        
        if (socialNetwork.Title.ToLower().Trim() != model.Title.ToLower().Trim())
        {
            if (await socialNetworkRepository.AnyAsync(s => s.Title.ToLower().Trim() == model.Title.ToLower().Trim()))
                return Result.Failure(string.Format(ErrorMessages.ConflictError, model.Title));
        }

        
        if (model.Icon != null && model.Icon.IsSvg())
        {
            socialNetwork.IconName.DeleteImage(FilePaths.SocialNetworkImage);
            var imageName = await model.Icon.AddImageToServer(FilePaths.SocialNetworkImage, checkImageFormat: false);
            model.IconName = imageName.Value;
        }

        socialNetwork.MapToSocialNetwork(model);
        socialNetworkRepository.Update(socialNetwork);
        await socialNetworkRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region Delete

    public async Task<Result> DeleteAsync(byte id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var socialNetwork = await socialNetworkRepository.GetByIdAsync(id);

        if (socialNetwork is null)
            return Result.Failure(ErrorMessages.NotFoundError);
        socialNetwork.IconName.DeleteImage(FilePaths.SocialNetworkImage);

        socialNetworkRepository.Delete(socialNetwork);
        await socialNetworkRepository.SaveChangesAsync();


        return Result.Success(SuccessMessages.DeleteSuccess);
    }

    #endregion

    #region GetAllAsync

    public async Task<Result<List<ClientSocialNetworkViewModel>>> GetAllAsync()
    {
        var socialNetworks = await socialNetworkRepository.GetAllAsync(s => s.MapToClientSocialNetworkViewModel());
        return socialNetworks;
    }

    #endregion
}