using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.UserSocialNetworkMapperMapping;
using BarnamenevisanCompany.Domain.Models.UserPosition;
using BarnamenevisanCompany.Domain.ViewModels.Admin.UserPosition;
using BarnamenevisanCompany.Domain.ViewModels.Client.UserPosition;
using Microsoft.IdentityModel.Tokens;

namespace BarnamenevisanCompany.Application.Mappers.UserPositionMappings;

static class UserPositionMapper
{
    public static AdminUserPositionViewModel MapToAdminUserPositionViewModel(this Domain.Models.UserPosition.UserPosition userPosition, short? projectId) =>
        new()
        {
            UserId = userPosition.UserId,
            Position = userPosition.Position,
            CreateDate = userPosition.CreatedDate,
            Id = userPosition.Id,
            Mobile = userPosition.Users.Mobile,
            Priority = userPosition.Priority,
            IsDeleted = userPosition.IsDeleted,
            WebSiteAddress = userPosition.WebsiteAddress,
            UserAvatarImageName = userPosition.Users.AvatarImageName,
            UserFullName = userPosition.Users.FirstName + " " + userPosition.Users.LastName,
        };

    // public static AdminUserPositionViewModel MapToUserPositionViewModel(this Domain.Models.UserPosition.UserPosition userPosition) =>

    public static Domain.Models.UserPosition.UserPosition MapToUserPosition(this AdminCreateUserPositionViewModel model) =>
        new()
        {
            UserId = model.UserId,
            Position = model.Position,
            WebsiteAddress = model.WebsiteAddress,
            Priority = model.Priority,
        };

    public static void MapToUserPosition(this Domain.Models.UserPosition.UserPosition userPosition, AdminUpdateUserPositionViewModel viewModel)
    {
        userPosition.Position = viewModel.Position;
        userPosition.WebsiteAddress = viewModel.WebsiteAddress;
        userPosition.Priority = viewModel.Priority;
    }

    public static AdminUpdateUserPositionViewModel MapToAdminUpdateUserPositionViewModel(this UserPosition userPosition) =>
        new()
        {
            UserId = userPosition.UserId,
            Position = userPosition.Position,
            WebsiteAddress = userPosition.WebsiteAddress,
            Priority = userPosition.Priority,
            Id = userPosition.Id,
        };

    public static ClientUserPositionViewModel MapToClientUserPositionViewModel(this UserPosition userPosition) =>
        new()
        {
            Position = userPosition.Position,
            WebsiteAddress = userPosition.WebsiteAddress,
            Priority = userPosition.Priority,
            ImageName = userPosition.Users.AvatarImageName,
            UserName = userPosition.Users.FirstName + " " + userPosition.Users.LastName,
            UserSocialNetworkViewModels = userPosition.Users.UserSocialNetworkMappings.Select(x => x.MapToUserPanelUserSocialNetworkViewModel()).ToList()
        };

    public static ClientProjectMemberViewModel MapToClientProjectMemberViewModel(this UserPosition userPosition, short projectId) =>
        new()
        {
            UserPosition = userPosition.ProjectMemberMappings.FirstOrDefault(s => s.ProjectId == projectId).UserPosition,
            SiteUrl = userPosition.WebsiteAddress,
            FullName = userPosition.Users.FirstName.IsNullOrEmpty() || userPosition.Users.LastName.IsNullOrEmpty() ? userPosition.Users.Mobile : userPosition.Users.FirstName + " " + userPosition.Users.LastName,
            Priority = userPosition.Priority,
            UserImage = userPosition.Users.AvatarImageName,
            UserSocialNetworkViewModels = userPosition.Users.UserSocialNetworkMappings.Select(x => x.MapToUserPanelUserSocialNetworkViewModel()).ToList()
        };

    public static void MapToRecoverUserPosition(this UserPosition userPosition, AdminCreateUserPositionViewModel model)
    {
        userPosition.IsDeleted = false;
        userPosition.DeletedDate = null;
        userPosition.Position = model.Position;
        userPosition.Priority = model.Priority;
        userPosition.WebsiteAddress = model.WebsiteAddress;
    }
}