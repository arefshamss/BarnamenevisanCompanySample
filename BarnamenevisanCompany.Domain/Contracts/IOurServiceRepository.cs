using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.OurServices;

namespace BarnamenevisanCompany.Domain.Contracts;

public interface IOurServiceRepository:IEfRepository<OurService,short>
{
    
}