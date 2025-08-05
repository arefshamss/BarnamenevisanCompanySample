using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.SocialNetwork;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class SocialNetworkRepository(BarnamenevisanContext context):EfRepository<SocialNetwork,byte>(context),ISocialNetworkRepository
{
    
}