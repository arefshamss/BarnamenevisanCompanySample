using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.UserSocialNetworkMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.UserPosition;
using BarnamenevisanCompany.Domain.Models.UserSocialNetworkMapper;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class UserSocialNetworkService(
    IUserSocialNetworkRepository userSocialNetworkRepository,
    IUserSocialNetworkMappingRepository userSocialNetworkMappingRepository) : IUserSocialNetworkService
{
    public async Task<SelectList> GetAllUserSocialNetworksAsync()
    {
        var userSocialNetworks = await userSocialNetworkRepository.GetAllAsync();

        var userSocial = userSocialNetworks.Select(x => x.MapToSelectViewModel()).ToList();

        return userSocial.ToSelectList();
    }

  
}