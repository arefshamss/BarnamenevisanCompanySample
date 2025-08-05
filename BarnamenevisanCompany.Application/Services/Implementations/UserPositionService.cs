using BarnamenevisanCompany.Application.Mappers.UserPositionMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.User;
using BarnamenevisanCompany.Domain.Models.User;
using BarnamenevisanCompany.Domain.Models.UserPosition;
using BarnamenevisanCompany.Domain.Models.UserSocialNetworkMapper;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.UserPosition;
using BarnamenevisanCompany.Domain.ViewModels.Client.UserPosition;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class UserPositionService(
    IUserPositionRepository userPositionRepository,
    IProjectMemberMappingRepository projectMemberMappingRepository,
    IUserSocialNetworkMappingRepository userSocialNetworkMappingRepository) : IUserPositionService
{
    #region Filter

    public async Task<AdminFilterUserPositionViewModel> FilterAsync(AdminFilterUserPositionViewModel filter, short? projectId)
    {
        var condition = Filter.GenerateConditions<UserPosition>();
        var orders = Filter.GenerateOrders<UserPosition>();

        if (!string.IsNullOrEmpty(filter.Position))
            condition.Add(u => EF.Functions.Like(u.Position, $"%{filter.Position}%"));

        condition.Add(s => !s.IsDeleted);

        switch (filter.UserPositionStatus)
        {
            case UserPositionStatus.None:
                break;
            case UserPositionStatus.DisplayPriority:
                orders.Add(u => u.Priority);
                break;
        }

        string[] includes =
        {
            nameof(UserPosition.Users),
            nameof(UserPosition.ProjectMemberMappings),
        };
        await userPositionRepository.FilterAsync(filter, condition, u => u.MapToAdminUserPositionViewModel(projectId), orders, includes: includes);
        return filter;
    }

    #endregion

    #region Create

    public async Task<Result> CreateAsync(AdminCreateUserPositionViewModel model)
    {
        if (model.Priority < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var priorityExist = await userPositionRepository.AnyAsync(p => p.Priority == model.Priority && !p.IsDeleted);
        if (priorityExist)
            return Result.Failure(ErrorMessages.PriorityExist);
        var userExist = await userPositionRepository.AnyAsync(p => p.UserId == model.UserId && !p.IsDeleted);
        if (userExist)
            return Result.Failure(ErrorMessages.AlreadyAdded);

        var userExistForRecover = await userPositionRepository.FirstOrDefaultAsync(p => p.UserId == model.UserId && p.IsDeleted);
        if (userExistForRecover != null)
        {
            userExistForRecover.MapToRecoverUserPosition(model);
            userPositionRepository.Update(userExistForRecover);
            await userPositionRepository.SaveChangesAsync();
            return Result.Success(SuccessMessages.InsertSuccessfullyDone);
        }

        var newUserPosition = model.MapToUserPosition();

        await userPositionRepository.InsertAsync(newUserPosition);
        await userPositionRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region Update

    public async Task<Result> UpdateAsync(AdminUpdateUserPositionViewModel model)
    {
        if (model.Priority < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var userPosition = await userPositionRepository.GetByIdAsync(model.Id);

        if (userPosition is null)
            return Result.Failure(ErrorMessages.NotFoundError);
        if (userPosition.Priority != model.Priority)
        {
            var priorityExist = await userPositionRepository.AnyAsync(p => p.Priority == model.Priority && !p.IsDeleted);
            if (priorityExist)
                return Result.Failure(ErrorMessages.PriorityExist);
        }

        userPosition.MapToUserPosition(model);


        userPositionRepository.Update(userPosition);
        await userPositionRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region DeleteOrRecover

    public async Task<Result> DeleteAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var userPosition = await userPositionRepository.GetByIdAsync(id);
        if (userPosition is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        await projectMemberMappingRepository.ExecuteDeleteRange(x => x.UserId == userPosition.Id);
        await userSocialNetworkMappingRepository.ExecuteDeleteRange(x => x.UserId == userPosition.UserId);

        userPositionRepository.SoftDelete(userPosition);


        await userPositionRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.DeleteSuccess);
    }

    #endregion

    #region FillModelForUpdate

    public async Task<Result<AdminUpdateUserPositionViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateUserPositionViewModel>(ErrorMessages.BadRequestError);

        var userPosition = await userPositionRepository.GetByIdAsync(id);
        if (userPosition is null)
            return Result.Failure<AdminUpdateUserPositionViewModel>(ErrorMessages.NotFoundError);

        return Result.Success(userPosition.MapToAdminUpdateUserPositionViewModel());
    }

    #endregion

    public async Task<ClientFilterUserPositionViewModel> GetAllClientUserPositionsAsync(ClientFilterUserPositionViewModel filter)
    {
        var condition = Filter.GenerateConditions<UserPosition>();
        condition.Add(u => !u.IsDeleted);
        var orders = Filter.GenerateOrders<UserPosition>();
        orders.Add(u => u.Priority);

        string[] includes =
        [
            nameof(UserPosition.Users),
            $"{nameof(UserPosition.Users)}.{nameof(User.UserSocialNetworkMappings)}",
            $"{nameof(UserPosition.Users)}.{nameof(User.UserSocialNetworkMappings)}.{nameof(UserSocialNetworkMapping.UserSocialNetwork)}",
        ];
        await userPositionRepository.FilterAsync(filter, condition, u => u.MapToClientUserPositionViewModel(), orders, includes: includes);
        return filter;
    }

    public async Task<Result<ClientUserPositionViewModel>> GetClientUserPositionByIdAsync(short id)
    {
        var user = await userPositionRepository.GetByIdAsync(id);
        if (user is null)
            return Result.Failure<ClientUserPositionViewModel>(ErrorMessages.NotFoundError);
        return user.MapToClientUserPositionViewModel();
    }

    public async Task<Result<int>> GetUserPositionsCountAsync()
    {
        return await userPositionRepository.CountAsync(x => !x.IsDeleted);
    }

    public async Task<bool> IsUserProgrammer(int userId)
    {
        if (userId < 1) return false;

        return await userPositionRepository.AnyAsync(x => x.UserId == userId && !x.IsDeleted);
    }
}