using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.UserSocialNetwork;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class UserSocialNetworkRepository(BarnamenevisanContext context):EfRepository<UserSocialNetwork,byte>(context),IUserSocialNetworkRepository
{
    
}