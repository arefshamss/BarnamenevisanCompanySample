using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.UserSocialNetworkMapper;

namespace BarnamenevisanCompany.Domain.Contracts;

public interface IUserSocialNetworkMappingRepository:IEfRepository<UserSocialNetworkMapping,short>
{
    
}