using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.User;
using BarnamenevisanCompany.Domain.ViewModels.Client.User;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IUserService
{
    #region Admin

    Task<Result<AdminFilterUsersViewModel>> FilterAsync(AdminFilterUsersViewModel filter);
    Task<Result<AdminUserViewModel>> GetByIdAsync(int id);
    Task<Result<AdminUpdateUserViewModel>> FillModelForUpdateAsync(int id);

    Task<Result> CreateAsync(AdminCreateUserViewModel model);
    Task<Result> UpdateAsync(AdminUpdateUserViewModel model);
    Task<Result> DeleteOrRecoverAsync(int id);

    #endregion

    #region Client

    Task<Result<ClientUserViewModel>> GetByIdForUserPanelAsync(int id);  
    Task<Result<ClientUpdateUserViewModel>> FillModelForUserPanelUpdateAsync(int id);
    Task<Result> UpdateAsync(ClientUpdateUserViewModel model);
    Task<Result> ChangePasswordAsync(ClientChangePasswordViewModel model);
    Task<Result<string>> UpdateAvatar(ClientUpdateUserAvatarViewModel model);   
    Task<Result<string>> DeleteAvatar(int id);     
    Task<Result<ClientOtpCodeForPasswordViewModel>> SendConfirmationCodeAsync(int userId);
    
    #endregion

    #region Account
    
    Task<Result<ConfirmLoginViewModel>> ConfirmLoginOrRegisterAsync(LoginOrRegisterViewModel model);  
    Task<Result<AuthenticateUserViewModel>> ConfirmOtpCodeAsync(OtpCodeViewModel model);
    Task<Result> SendOtpCodeSmsAsync(string message, string mobile);  
    Task<Result<ResendOtpCodeViewModel>> ResendOtpCodeSmsAsync(int userId);
    Task<Result<string>> GetActiveCodeExpireTimeCodeAsync(string mobile);       

    #endregion
}