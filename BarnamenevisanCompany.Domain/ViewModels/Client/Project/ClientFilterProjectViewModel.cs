using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.OurService;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Project;

public class ClientFilterProjectViewModel : BasePaging<ClientProjectViewModel>
{
    public FilterOrderBy FilterOrderBy { get; set; }    
}