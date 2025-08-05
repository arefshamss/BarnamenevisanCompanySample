using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IUserSocialNetworkService
{
    Task<SelectList> GetAllUserSocialNetworksAsync();
    
    
}