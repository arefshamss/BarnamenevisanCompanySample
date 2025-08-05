using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.UserMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.User;
using BarnamenevisanCompany.Domain.Models.User;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.User;
using BarnamenevisanCompany.Domain.ViewModels.Client.User;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class UserService(
    IUserRepository userRepository,
    ISmsService smsService,
    IUserPositionRepository userPositionRepository,
    IProjectMemberMappingRepository projectMemberMappingRepository,
    IUserSocialNetworkMappingRepository userSocialNetworkMappingRepository) : IUserService
{
    #region Admin

    public async Task<Result<AdminFilterUsersViewModel>> FilterAsync(AdminFilterUsersViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<User>();
        var orders = Filter.GenerateOrders<User>();

        #region Filter

        if (!string.IsNullOrWhiteSpace(filter.FirstName))
            conditions.Add(x => EF.Functions.Like(x.FirstName, $"%{filter.FirstName}%"));

        if (!string.IsNullOrWhiteSpace(filter.LastName))
            conditions.Add(x => EF.Functions.Like(x.LastName, $"%{filter.LastName}%"));

        if (!string.IsNullOrWhiteSpace(filter.Mobile))
            conditions.Add(x => EF.Functions.Like(x.Mobile, $"%{filter.Mobile}%"));

        switch (filter.UserActiveStatus)
        {
            case UserActiveStatus.Active:
                conditions.Add(x => x.IsActive);
                break;

            case UserActiveStatus.Disabled:
                conditions.Add(x => !x.IsActive);
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

        switch (filter.DeleteStatus)
        {
            case DeleteStatus.Deleted:
                conditions.Add(x => x.IsDeleted);
                break;

            case DeleteStatus.NotDeleted:
                conditions.Add(x => !x.IsDeleted);
                break;
        }

        #endregion

        await userRepository.FilterAsync(filter, conditions, x => x.MapToAdminUserViewModel(), orders);
        return filter;
    }

    public async Task<Result<AdminUserViewModel>> GetByIdAsync(int id)
    {
        if (id < 1)
            return Result.Failure<AdminUserViewModel>(ErrorMessages.BadRequestError);

        var user = await userRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (user is null)
            return Result.Failure<AdminUserViewModel>(ErrorMessages.NotFoundError);

        return user.MapToAdminUserViewModel();
    }

    public async Task<Result<AdminUpdateUserViewModel>> FillModelForUpdateAsync(int id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateUserViewModel>(ErrorMessages.BadRequestError);

        var user = await userRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (user is null)
            return Result.Failure<AdminUpdateUserViewModel>(ErrorMessages.NotFoundError);

        return user.MapToAdminUpdateUserViewModel();
    }


    public async Task<Result> CreateAsync(AdminCreateUserViewModel model)
    {
        #region Validations

        if (!model.Mobile.IsNullOrEmptyOrWhiteSpace() && await userRepository.AnyAsync(x => x.Mobile == model.Mobile && x.IsActive && !x.IsDeleted))
            return Result.Failure(string.Format(ErrorMessages.ConflictError, "شماره موبایل"));

        #endregion

        var user = model.MapToUser();

        #region Add User Avatar

        if (model.AvatarImageFile is not null)
        {
            var result = await model.AvatarImageFile.AddImageToServer(FilePaths.UserOriginalAvatar, 300, 300,
                FilePaths.UserThumbAvatar);

            if (result.IsFailure)
                return Result.Failure(result.Message!);

            user.AvatarImageName = result.Value;
        }

        #endregion

        await userRepository.InsertAsync(user);
        await userRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    public async Task<Result> UpdateAsync(AdminUpdateUserViewModel model)
    {
        #region Validations

        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        if (model.IsActive && await userRepository.AnyAsync(x => x.Mobile == model.Mobile && x.Id != model.Id && x.IsActive && !x.IsDeleted))
            return Result.Failure(string.Format(ErrorMessages.ConflictActiveUserError, "ویرایش"));

        #endregion

        var user = await userRepository.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);

        if (user is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        user.MapToUser(model);
        if(!model.Password.IsNullOrEmptyOrWhiteSpace())
            user.Password = model.Password!.HashPassword();
        
        #region Add User Avatar

        if (model.AvatarImageFile is not null)
        {
            var result = await model.AvatarImageFile.AddImageToServer(FilePaths.UserOriginalAvatar, 300, 300,
                FilePaths.UserThumbAvatar, deleteFileName: user.AvatarImageName);

            if (result.IsFailure)
                return Result.Failure(result.Message!);

            user.AvatarImageName = result.Value;
        }

        #endregion

        userRepository.Update(user);
        await userRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    public async Task<Result> DeleteOrRecoverAsync(int id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var user = await userRepository.GetByIdAsync(id);

        if (user is null)
            return Result.Failure(ErrorMessages.NotFoundError);
        
        if (!user.Mobile.IsNullOrEmptyOrWhiteSpace() && await userRepository.AnyAsync(x => x.Mobile == user.Mobile && x.Id != user.Id && x.IsActive && !x.IsDeleted))
            return Result.Failure(string.Format(ErrorMessages.ConflictActiveUserError, "بازگردانی"));


        var result = userRepository.SoftDeleteOrRecover(user);

        #region HardDeleteUserPositionAndProjecttMemberMapping

        if (result)
        {
            if (await userPositionRepository.AnyAsync(s => s.UserId == id))
            {
                var userPosition = await userPositionRepository.FirstOrDefaultAsync(s => s.UserId == id);
                if (userPosition is null)
                    return Result.Failure(ErrorMessages.NotFoundError);

                userPositionRepository.SoftDelete(userPosition);

                await projectMemberMappingRepository.ExecuteDeleteRange(x => x.UserId == userPosition.Id);

                await userSocialNetworkMappingRepository.ExecuteDeleteRange(x => x.UserId == id);

                await userRepository.SaveChangesAsync();

                return Result.Success(SuccessMessages.DeleteSuccess);
            }
        }

        #endregion

        await userRepository.SaveChangesAsync();


        return Result.Success(SuccessMessages.RecoverSuccess);
    }

    #endregion

    #region Account

    public async Task<Result<ConfirmLoginViewModel>> ConfirmLoginOrRegisterAsync(LoginOrRegisterViewModel model)
    {
        if (model.IsLoginByPassword && model.Password.IsNullOrEmptyOrWhiteSpace())
            return Result.Failure<ConfirmLoginViewModel>(string.Format(ErrorMessages.RequiredError, "رمز عبور"));

        var user = await userRepository.FirstOrDefaultAsync(x => x.Mobile == model.Mobile.Trim() && !x.IsDeleted);
        string successMessage;

        if (model.IsLoginByPassword)
        {
            if (user is null)
                return Result.Failure<ConfirmLoginViewModel>(ErrorMessages.UserNotFoundError);

            if(!user.IsActive)
                return Result.Failure<ConfirmLoginViewModel>(ErrorMessages.UserNotActiveError);
            
            if (!model.Password!.VerifyPassword(user.Password))
                return Result.Failure<ConfirmLoginViewModel>(ErrorMessages.UserNotFoundError);
            
            successMessage = SuccessMessages.LoginSuccessfullyDone;
        }
        else
        {
            if (user is null)
            {
                if (await userRepository.AnyAsync(x => x.Mobile == model.Mobile.Trim() && !x.IsDeleted))
                    return Result.Failure<ConfirmLoginViewModel>(string.Format(ErrorMessages.ConflictError, "شماره همراه"));

                user = model.MapToUser();

                await userRepository.InsertAsync(user);
                await userRepository.SaveChangesAsync();
            }
            
            if(!user.IsActive)
                return Result.Failure<ConfirmLoginViewModel>(ErrorMessages.UserNotActiveError);
            
            user.ActiveCode = Common.GenerateRandomNumericCode(6);
            user.ActiveCodeExpireTime = DateTime.Now.AddMinutes(1.5);

            userRepository.Update(user);
            await userRepository.SaveChangesAsync();

            successMessage = SuccessMessages.OtpSentSuccessfullyDone;
        }

        return Result.Success(user.MapToConfirmLoginViewModel(model.IsLoginByPassword), successMessage);
    }

    public async Task<Result<AuthenticateUserViewModel>> ConfirmOtpCodeAsync(OtpCodeViewModel model)
    {
        if (model.UserId < 1)
            return Result.Failure<AuthenticateUserViewModel>(ErrorMessages.SomethingWentWrong);

        var user = await userRepository.FirstOrDefaultAsync(s => s.Id == model.UserId && !s.IsDeleted);

        if (user is null)
            return Result.Failure<AuthenticateUserViewModel>(ErrorMessages.BadRequestError);

        if (user.ActiveCodeExpireTime < DateTime.Now)
            return Result.Failure<AuthenticateUserViewModel>(ErrorMessages.ExpireConfirmCodeError);

        if (user.ActiveCode is null || user.ActiveCode != model.Code)
            return Result.Failure<AuthenticateUserViewModel>(ErrorMessages.InvalidConfirmationCode);

        user.ActiveCode = null;
        user.ActiveCodeExpireTime = null;
        if (!user.IsActive)
            user.IsActive = true;

        userRepository.Update(user);
        await userRepository.SaveChangesAsync();

        return user.MapToAuthenticateUserViewModel();
    }

    public async Task<Result> SendOtpCodeSmsAsync(string message, string mobile)
    {
        Result smsSendResult;
        byte retryCount = 0;
        do
        {
            smsSendResult = await smsService.SendSmsAsync(mobile, message);
            retryCount++;
        } while (smsSendResult.IsFailure && retryCount <= 5);

        return smsSendResult.IsSuccess
            ? Result.Success(SuccessMessages.OtpSentSuccessfullyDone)
            : Result.Failure(message: ErrorMessages.SmsDidNotSendError);
    }

    public async Task<Result<ResendOtpCodeViewModel>> ResendOtpCodeSmsAsync(int userId)
    {
        if (userId < 1)
            return Result.Failure<ResendOtpCodeViewModel>(ErrorMessages.BadRequestError);

        var user = await userRepository.FirstOrDefaultAsync(s => s.Id == userId && !s.IsDeleted);

        if (user is null) return Result.Failure<ResendOtpCodeViewModel>(ErrorMessages.UserNotFoundError);

        if (string.IsNullOrEmpty(user.Mobile))
            return Result.Failure<ResendOtpCodeViewModel>(ErrorMessages.SomethingWentWrong);

        if (DateTime.Now < user.ActiveCodeExpireTime)
            return Result.Failure<ResendOtpCodeViewModel>(ErrorMessages.ActiveCodeExpireDateTime);

        var randomCode = Common.GenerateRandomNumericCode(6);
        while (user.ActiveCode == randomCode)
        {
            randomCode = Common.GenerateRandomNumericCode(6);
        }

        user.ActiveCode = randomCode;
        user.ActiveCodeExpireTime = DateTime.Now.AddMinutes(1.5);
        userRepository.Update(user);
        await userRepository.SaveChangesAsync();

        return (await SendOtpCodeSmsAsync($"کد تایید شما جهت ورود به برنامه نویسان {user.ActiveCode}", user.Mobile)).IsSuccess
            ? Result.Success(user.MapToResendOtpCodeViewModel(), SuccessMessages.OtpSentSuccessfullyDone)
            : Result.Failure<ResendOtpCodeViewModel>(ErrorMessages.SmsDidNotSendError);
    }

    public async Task<Result<string>> GetActiveCodeExpireTimeCodeAsync(string mobile) =>
        (await userRepository.FirstOrDefaultAsync(x => x.Mobile == mobile && !x.IsDeleted))!.ActiveCodeExpireTime!.Value.ToJavaScriptDateTimeStandard();

    #endregion

    #region Client

    public async Task<Result<ClientUserViewModel>> GetByIdForUserPanelAsync(int id)
    {
        if (id < 1)
            return Result.Failure<ClientUserViewModel>(ErrorMessages.BadRequestError);

        var user = await userRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (user is null)
            return Result.Failure<ClientUserViewModel>(ErrorMessages.NotFoundError);

        return user.MapToClientUserViewModel();
    }

    public async Task<Result<ClientUpdateUserViewModel>> FillModelForUserPanelUpdateAsync(int id)
    {
        if (id < 1)
            return Result.Failure<ClientUpdateUserViewModel>(ErrorMessages.BadRequestError);

        var user = await userRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (user is null)
            return Result.Failure<ClientUpdateUserViewModel>(ErrorMessages.NotFoundError);

        return user.MapToClientUpdateUserViewModel();
    }

    public async Task<Result> UpdateAsync(ClientUpdateUserViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var user = await userRepository.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);

        if (user is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        user.MapToUser(model);

        userRepository.Update(user);
        await userRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    public async Task<Result> ChangePasswordAsync(ClientChangePasswordViewModel model)
    {
        if (model.UserId < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var user = await userRepository.FirstOrDefaultAsync(x => x.Id == model.UserId && !x.IsDeleted);

        if (user is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        user.Password = model.NewPassword.HashPassword();
        await UpdateUserPasswordAsync(user);

        return Result.Success(SuccessMessages.ChangePasswordSuccess);
    }

    public async Task<Result<string>> UpdateAvatar(ClientUpdateUserAvatarViewModel model)
    {
        if (model.UserId < 1)
            return Result.Failure<string>(ErrorMessages.BadRequestError);

        var user = await userRepository.FirstOrDefaultAsync(x => x.Id == model.UserId && !x.IsDeleted);

        if (user is null)
            return Result.Failure<string>(ErrorMessages.NotFoundError);

        var result = await model.Avatar.AddImageToServer(FilePaths.UserOriginalAvatar, 300, 300, FilePaths.UserThumbAvatar, deleteFileName: user.AvatarImageName);
        if (result.IsFailure)
            return Result.Failure<string>(result.Message);

        user.AvatarImageName = result.Value;

        userRepository.Update(user);
        await userRepository.SaveChangesAsync();

        return Result.Success(FilePaths.UserOriginalAvatar + result.Value, SuccessMessages.UpdateAvatarSuccessfullyDone);
    }

    public async Task<Result<string>> DeleteAvatar(int id)
    {
        if (id < 1)
            return Result.Failure<string>(ErrorMessages.BadRequestError);

        var user = await userRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (user is null)
            return Result.Failure<string>(ErrorMessages.NotFoundError);

        user.AvatarImageName?.DeleteImage(FilePaths.UserOriginalAvatar, FilePaths.UserThumbAvatar);
        user.AvatarImageName = null;

        userRepository.Update(user);
        await userRepository.SaveChangesAsync();

        return Result.Success(FilePaths.UserOriginalAvatar + SiteTools.DefaultUserImageName, SuccessMessages.DeleteAvatarSuccessfullyDone);
    }


    public async Task<Result<ClientOtpCodeForPasswordViewModel>> SendConfirmationCodeAsync(int userId)
    {
        if (userId < 1)
            return Result.Failure<ClientOtpCodeForPasswordViewModel>(ErrorMessages.BadRequestError);

        var user = await userRepository.FirstOrDefaultAsync(s => s.Id == userId && !s.IsDeleted);

        if (user is null) return Result.Failure<ClientOtpCodeForPasswordViewModel>(ErrorMessages.UserNotFoundError);

        if (string.IsNullOrEmpty(user.Mobile))
            return Result.Failure<ClientOtpCodeForPasswordViewModel>(ErrorMessages.SomethingWentWrong);

        if (DateTime.Now < user.ActiveCodeExpireTime)
            return Result.Failure<ClientOtpCodeForPasswordViewModel>(ErrorMessages.ActiveCodeExpireDateTime);

        var randomCode = Common.GenerateRandomNumericCode(6);
        while (user.ActiveCode == randomCode)
        {
            randomCode = Common.GenerateRandomNumericCode(6);
        }

        user.ActiveCode = randomCode;
        user.ActiveCodeExpireTime = DateTime.Now.AddMinutes(1.5);
        userRepository.Update(user);
        await userRepository.SaveChangesAsync();

        return (await SendOtpCodeSmsAsync(string.Format(SmsMessages.ConfirmationCodeForPassword,user.ActiveCode), user.Mobile)).IsSuccess
            ? Result.Success(user.MapToClientOtpCodeForPasswordViewModel(), SuccessMessages.OtpSentSuccessfullyDone)
            : Result.Failure<ClientOtpCodeForPasswordViewModel>(ErrorMessages.SmsDidNotSendError);
    }

    #endregion

    #region Helpers

    private async Task UpdateUserPasswordAsync(User user)
    {
        userRepository.Update(user);
        await userRepository.SaveChangesAsync();
    }

    #endregion
}