using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.OurServices;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class OurServiceRepository(BarnamenevisanContext context):EfRepository<OurService,short>(context),IOurServiceRepository
{
    
}