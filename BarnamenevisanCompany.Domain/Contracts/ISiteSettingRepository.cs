using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.SiteSetting;

namespace BarnamenevisanCompany.Domain.Contracts;

public interface ISiteSettingRepository:IEfRepository<SiteSetting,byte>
{
    
}