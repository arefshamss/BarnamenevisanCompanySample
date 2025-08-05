using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.UserSocialNetwork;

namespace BarnamenevisanCompany.Domain.Contracts;

public interface IUserSocialNetworkRepository:IEfRepository<UserSocialNetwork,byte>
{
    
}