using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.ContactUs;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class ContactUsRepository(BarnamenevisanContext context):EfRepository<ContactUs,short>(context),IContactUsRepository
{
    
}