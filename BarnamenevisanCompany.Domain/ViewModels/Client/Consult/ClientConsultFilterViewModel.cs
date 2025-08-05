using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Consult;

public class ClientConsultFilterViewModel:BasePaging<ClientConsultListUserViewModel>
{
    [Display(Name = "عنوان")]
    [FilterInput]
    public string Title { get; set; }
}