using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.SiteSetting;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class SiteSettingRepository(BarnamenevisanContext context):EfRepository<SiteSetting,byte>(context),ISiteSettingRepository
{
    
}