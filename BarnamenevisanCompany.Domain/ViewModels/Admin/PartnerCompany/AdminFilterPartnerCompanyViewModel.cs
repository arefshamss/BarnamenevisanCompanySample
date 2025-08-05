using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Company;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.PartnerCompany;

public class AdminFilterPartnerCompanyViewModel : BasePaging<AdminPartnerCompanyViewModel>
{
    [Display(Name="عنوان سایت"), FilterInput]
    public string? Title { get; set; }
    
    
    [Display(Name="وضعیت حذف"),FilterInput]
    public DeleteStatus DeleteStatus { get; set; }
}