using BarnamenevisanCompany.Domain.Models.UserSocialNetwork;
using BarnamenevisanCompany.Domain.Models.UserSocialNetworkMapper;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Application.Mappers.UserSocialNetworkMappings;

public static class UserSocialNetworkMapperMapper
{
    public static SelectViewModel<byte> MapToSelectViewModel(this UserSocialNetwork userSocialNetwork) =>
        new()
        {
            Id = userSocialNetwork.Id,
            DisplayValue = userSocialNetwork.PersianTitle
        };

}