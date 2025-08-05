using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Honors;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class HonorsRepository(BarnamenevisanContext context):EfRepository<Honors,short>(context),IHonorsRepository
{
    
}