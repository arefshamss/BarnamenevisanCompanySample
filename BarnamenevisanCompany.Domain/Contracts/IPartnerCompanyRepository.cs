using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.Company;

namespace BarnamenevisanCompany.Domain.Contracts;

public interface IPartnerCompanyRepository : IEfRepository<PartnerCompany,short>
{
}