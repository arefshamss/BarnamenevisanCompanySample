using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.JobUserMapping;

public class ClientFilterJobUserMappingViewModel : BasePaging<ClientJobUserMappingViewModel>
{
    public int UserId { get; set; }
}