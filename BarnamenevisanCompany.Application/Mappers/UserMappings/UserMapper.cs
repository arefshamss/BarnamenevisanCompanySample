using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Domain.Extensions;
using BarnamenevisanCompany.Domain.Models.User;
using BarnamenevisanCompany.Domain.ViewModels.Admin.User;
using BarnamenevisanCompany.Domain.ViewModels.Client.User;

namespace BarnamenevisanCompany.Application.Mappers.UserMappings;

public static class UserMapper
{
    #region Admin

    public static AdminUserViewModel MapToAdminUserViewModel(this User model) =>
        new()
        {
            Id = model.Id,
            Mobile = model.Mobile,
            FirstName = model.FirstName,
            LastName = model.LastName,
            IsActive = model.IsActive,
            AvatarImageName = model.AvatarImageName,
            CreatedDate = model.CreatedDate,
            IsDeleted = model.IsDeleted,
        };

    public static User MapToUser(this AdminCreateUserViewModel model) =>
        new()
        {
            Mobile = model.Mobile,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Password = model.Password?.HashPassword(),
            ActiveCode = Common.GenerateRandomNumericCode(6),
            IsActive = true
        };

    public static void MapToUser(this User user, AdminUpdateUserViewModel model)
    {
        user.IsActive = model.IsActive;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
    }

    public static AdminUpdateUserViewModel MapToAdminUpdateUserViewModel(this User model) =>
        new()
        {
            Id = model.Id,
            Mobile = model.Mobile,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Password = model.Password,
            AvatarImageName = model.AvatarImageName,
            IsActive = model.IsActive
        };

    #endregion

    #region Account

    public static ConfirmLoginViewModel MapToConfirmLoginViewModel(this User model, bool isLoginByPassword) =>
        new()
        {
            Mobile = model.Mobile,
            FullName = model.GetUserDisplayName(),
            UserId = model.Id,
            ActiveCode = model.ActiveCode,
            IsLoginByPassword = isLoginByPassword
        };

    public static AuthenticateUserViewModel MapToAuthenticateUserViewModel(this User model) => 
        new()
        {
            Mobile = model.Mobile,
            FullName = model.GetUserDisplayName(),
            UserId = model.Id,
        };
    

    public static ResendOtpCodeViewModel MapToResendOtpCodeViewModel(this User model) =>
        new()
        {
            ActiveCodeExpireDateTime = model.ActiveCodeExpireTime!.Value.ToJavaScriptDateTimeStandard()
        };
    
    public static ClientOtpCodeForPasswordViewModel MapToClientOtpCodeForPasswordViewModel(this User model) =>
        new()
        {
            ActiveCodeExpireDateTime = model.ActiveCodeExpireTime!.Value.ToJavaScriptDateTimeStandard()
        };

    public static User MapToUser(this LoginOrRegisterViewModel model) =>
        new()
        {
            Mobile = model.Mobile,
            IsActive = true
        };

    #endregion

    #region Client

    public static ClientUserViewModel MapToClientUserViewModel(this User model) =>
        new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Mobile = model.Mobile,
            AvatarImageName = model.AvatarImageName,
            CreatedDate = model.CreatedDate
        };

    public static ClientUpdateUserViewModel MapToClientUpdateUserViewModel(this User model) =>
        new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Mobile = model.Mobile
        };

    public static void MapToUser(this User user, ClientUpdateUserViewModel model)
    {
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
    }

    #endregion
}