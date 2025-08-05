using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.UserPosition;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class UserPositionRepository(BarnamenevisanContext context):EfRepository<UserPosition,short>(context),IUserPositionRepository
{
    
}