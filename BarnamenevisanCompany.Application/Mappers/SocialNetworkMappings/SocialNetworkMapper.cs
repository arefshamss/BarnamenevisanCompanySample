using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Domain.Models.SocialNetwork;
using BarnamenevisanCompany.Domain.ViewModels.Admin.SocialNetwork;
using BarnamenevisanCompany.Domain.ViewModels.Client.SocialNetwork;

namespace BarnamenevisanCompany.Application.Mappers.SocialNetworkMappings;

public static class SocialNetworkMapper
{
    public static SocialNetwork MapToSocialNetwork(this AdminCreateSocialNetworkViewModel model) =>
        new()
        {
            IconName = model.IconName,
            Title = model.Title,
            Url = model.Url.NormalizeSiteUrl(),
        };

    public static AdminSocialNetworkViewModel MapToSocialNetworkViewModel(this SocialNetwork socialNetwork) =>
        new()
        {
            IconName = socialNetwork.IconName,
            Title = socialNetwork.Title,
            Url = socialNetwork.Url,
            Id = socialNetwork.Id,
            CreateDate = socialNetwork.CreatedDate,
        };

    public static void MapToSocialNetwork(this SocialNetwork model, AdminSocialNetworkUpdateViewModel viewModel)
    {
        model.IconName = viewModel.IconName;
        model.Title = viewModel.Title;
        model.Url = viewModel.Url.NormalizeSiteUrl();
    }

    public static AdminSocialNetworkUpdateViewModel MapToAdminSocialNetworkUpdateViewModel(this SocialNetwork model) =>
        new()
        {
            IconName = model.IconName,
            Title = model.Title,
            Url = model.Url,
            Id = model.Id,
        };

    public static ClientSocialNetworkViewModel MapToClientSocialNetworkViewModel(this SocialNetwork socialNetwork) =>
        new()
        {
            SocialName = socialNetwork.Title,
            IconName = socialNetwork.IconName,
            Url = socialNetwork.Url
        };
}