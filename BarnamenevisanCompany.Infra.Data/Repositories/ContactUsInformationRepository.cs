using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.ContactUs;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class ContactUsInformationRepository(BarnamenevisanContext context):EfRepository<ContactUsInformation,short>(context),IContactUsInformationRepository
{
    
}