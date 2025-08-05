using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.UserSocialNetworkMapper;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class UserSocialNetworkMappingRepository(BarnamenevisanContext context):EfRepository<UserSocialNetworkMapping,short>(context),IUserSocialNetworkMappingRepository
{
    
}