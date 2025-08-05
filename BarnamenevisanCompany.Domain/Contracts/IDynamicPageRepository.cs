using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.DynamicPage;

namespace BarnamenevisanCompany.Domain.Contracts;

public interface IDynamicPageRepository : IEfRepository<DynamicPage,short>
{
    
}