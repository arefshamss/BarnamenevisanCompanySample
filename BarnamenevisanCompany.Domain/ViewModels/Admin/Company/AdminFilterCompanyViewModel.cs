using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Company;

public class AdminFilterCompanyViewModel : BasePaging<AdminCompanyViewModel>
{
    [Display(Name="نام شرکت"), FilterInput]
    public string? Title { get; set; }
    
    
    [Display(Name="وضعیت حذف"),FilterInput]
    public DeleteStatus DeleteStatus { get; set; }
}