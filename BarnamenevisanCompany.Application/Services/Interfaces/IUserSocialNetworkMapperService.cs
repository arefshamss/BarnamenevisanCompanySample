using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.UserSocialNetwork;
using BarnamenevisanCompany.Domain.ViewModels.Client.UserSocialNetworkMapper;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IUserSocialNetworkMapperService
{
    Task<Result> CreateAsync(ClientCreateSocialNetworkMapperViewModel model);

    Task<Result<ClientFilterUserSocialNetworkListViewModel>> FilterAsync(ClientFilterUserSocialNetworkListViewModel filter);

    Task<Result<ClientUpdateUserSocialNetworkViewModel>> FillModelForUpdateAsync(byte id,int userId);
    
    Task<Result> UpdateAsync(ClientUpdateUserSocialNetworkViewModel model,int userId);
    
    Task<Result> DeleteAsync(byte id,int userId);
}