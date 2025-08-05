using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.UserPosition;

namespace BarnamenevisanCompany.Domain.Contracts;

public interface IUserPositionRepository:IEfRepository<UserPosition,short>
{
    
}