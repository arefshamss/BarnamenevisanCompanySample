using BarnamenevisanCompany.Domain.Models.SocialNetwork;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.SocialNetwork;
using BarnamenevisanCompany.Domain.ViewModels.Client.SocialNetwork;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface ISocialNetworkService
{
    Task<AdminFilterSocialNetworkViewModel> FilterAsync(AdminFilterSocialNetworkViewModel filter);

    Task<Result> CreateAsync(AdminCreateSocialNetworkViewModel model);

    Task<Result<AdminSocialNetworkUpdateViewModel>> FillModelForUpdateAsync(byte id);

    Task<Result> UpdateAsync(AdminSocialNetworkUpdateViewModel model);

    Task<Result> DeleteAsync(byte id);

    Task<Result<List<ClientSocialNetworkViewModel>>> GetAllAsync();
}