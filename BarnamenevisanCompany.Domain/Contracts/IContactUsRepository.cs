using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.ContactUs;

namespace BarnamenevisanCompany.Domain.Contracts;

public interface IContactUsRepository:IEfRepository<ContactUs,short>
{
    
}