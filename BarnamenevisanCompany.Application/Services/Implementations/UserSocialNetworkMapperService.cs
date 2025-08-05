using BarnamenevisanCompany.Application.Mappers.UserSocialNetworkMapperMapping;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.UserSocialNetwork;
using BarnamenevisanCompany.Domain.Models.UserSocialNetworkMapper;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.UserSocialNetwork;
using BarnamenevisanCompany.Domain.ViewModels.Client.UserSocialNetworkMapper;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class UserSocialNetworkMapperService(
    IUserSocialNetworkMappingRepository userSocialNetworkMappingRepository,
    IUserPositionRepository userPositionRepository) : IUserSocialNetworkMapperService
{
    #region CreateAsync

    public async Task<Result> CreateAsync(ClientCreateSocialNetworkMapperViewModel model)
    {
        if (model.UserId < 1 || model.SocialNetworkId < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        if (!await userPositionRepository.AnyAsync(x => x.UserId == model.UserId))
            return Result.Failure(ErrorMessages.AccessDenied);

        if (await userSocialNetworkMappingRepository.AnyAsync(x => x.UserId == model.UserId && x.SocialNetworkId == model.SocialNetworkId))
            return Result.Failure(string.Format(ErrorMessages.AlreadyExistError,"شبکه اجتماعی"));

        await userSocialNetworkMappingRepository.InsertAsync(model.MapToUserPanelCreateSocialNetworkMapperViewModel());
        await userSocialNetworkMappingRepository.SaveChangesAsync();
        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region GetAllAsync

    public async Task<Result<ClientFilterUserSocialNetworkListViewModel>> FilterAsync(ClientFilterUserSocialNetworkListViewModel filter)
    {
        var condition = Filter.GenerateConditions<UserSocialNetworkMapping>();

        #region Confitions

        condition.Add(s => !s.IsDeleted);
        condition.Add(x => x.UserId == filter.UserId);

        #endregion
        
        await userSocialNetworkMappingRepository
            .FilterAsync(filter, condition, s => s.MapToUserPanelUserSocialNetworkListViewModel(), includes: [nameof(UserSocialNetworkMapping.UserSocialNetwork)]);
        return filter;
    }

    #endregion

    #region FillModelForUpdate

    public async Task<Result<ClientUpdateUserSocialNetworkViewModel>> FillModelForUpdateAsync(byte id, int userId)
    {
        if (id < 1)
            return Result.Failure<ClientUpdateUserSocialNetworkViewModel>(ErrorMessages.BadRequestError);
        var social = await userSocialNetworkMappingRepository.FirstOrDefaultAsync(s => s.UserId == userId && s.SocialNetworkId == id, includes: nameof(UserSocialNetworkMapping.UserSocialNetwork));
        if (social == null)
            return Result.Failure<ClientUpdateUserSocialNetworkViewModel>(ErrorMessages.NotFoundError);

        return social.MapToUpdateUserSocialNetworkViewModel();
    }

    #endregion

    #region UpdateAsync

    public async Task<Result> UpdateAsync(ClientUpdateUserSocialNetworkViewModel model, int userId)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);
        var social = await userSocialNetworkMappingRepository.FirstOrDefaultAsync(s => s.SocialNetworkId == model.Id && s.UserId == userId);

        if (social == null)
            return Result.Failure(ErrorMessages.NotFoundError);

        social.MapToUserSocialNetworkMapper(model);

        userSocialNetworkMappingRepository.Update(social);

        await userSocialNetworkMappingRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region DeleteAsync

    public async Task<Result> DeleteAsync(byte id, int userId)
    {
        if (id < 1 || userId < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var social = await userSocialNetworkMappingRepository.FirstOrDefaultAsync(s => s.UserId == userId && s.SocialNetworkId == id);

        if (social == null)
            return Result.Failure(ErrorMessages.NotFoundError);
        userSocialNetworkMappingRepository.Delete(social);
        await userSocialNetworkMappingRepository.SaveChangesAsync();
        return Result.Success(SuccessMessages.DeleteSuccess);
    }

    #endregion
}