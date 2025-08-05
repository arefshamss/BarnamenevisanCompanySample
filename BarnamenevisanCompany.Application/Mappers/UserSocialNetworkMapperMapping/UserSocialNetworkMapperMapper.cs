using BarnamenevisanCompany.Domain.Models.UserSocialNetwork;
using BarnamenevisanCompany.Domain.Models.UserSocialNetworkMapper;
using BarnamenevisanCompany.Domain.ViewModels.Client.UserSocialNetwork;
using BarnamenevisanCompany.Domain.ViewModels.Client.UserSocialNetworkMapper;

namespace BarnamenevisanCompany.Application.Mappers.UserSocialNetworkMapperMapping;

public static class UserSocialNetworkMapperMapper
{
    public static UserSocialNetworkMapping MapToUserPanelCreateSocialNetworkMapperViewModel(this ClientCreateSocialNetworkMapperViewModel model) =>
        new()
        {
            SocialLink = model.SocialLink,
            SocialNetworkId = model.SocialNetworkId,
            UserId = model.UserId,
        };

    public static ClientUserSocialNetworkViewModel MapToUserPanelUserSocialNetworkViewModel(this UserSocialNetworkMapping model) =>
        new()
        {
            SocialLink = model.SocialLink,
            ImageName = model.UserSocialNetwork.ImageName,
        };

    public static ClientUserSocialNetworkListViewModel MapToUserPanelUserSocialNetworkListViewModel(this UserSocialNetworkMapping model) =>
        new()
        {
            ImageName = model.UserSocialNetwork.ImageName,
            SocialUrl = model.SocialLink,
            Title = model.UserSocialNetwork.PersianTitle,
            Id = model.UserSocialNetwork.Id,
        };

    public static ClientUpdateUserSocialNetworkViewModel MapToUpdateUserSocialNetworkViewModel(this UserSocialNetworkMapping model) =>
        new()
        {
            ImageName = model.UserSocialNetwork.ImageName,
            SocialUrl = model.SocialLink,
            Title = model.UserSocialNetwork.PersianTitle,
            Id = model.UserSocialNetwork.Id
        };

    public static void MapToUserSocialNetworkMapper(this UserSocialNetworkMapping model, ClientUpdateUserSocialNetworkViewModel viewModel)
    {
        model.SocialLink = viewModel.SocialUrl;
    }
}