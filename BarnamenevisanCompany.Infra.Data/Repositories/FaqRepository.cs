using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Faq;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class FaqRepository(BarnamenevisanContext context) : EfRepository<Faq, short>(context), IFaqRepository
{
}