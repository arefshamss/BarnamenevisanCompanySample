using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.DynamicPage;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class DynamicPageRepository(BarnamenevisanContext context) : EfRepository<DynamicPage, short>(context), IDynamicPageRepository
{
}