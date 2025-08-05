using BarnamenevisanCompany.Domain.Enums.Consult;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Consult;

public class ClientConsultListUserViewModel
{
    public short Id { get; set; }
    public string Title { get; set; }
    
    public ConsultStatus ConsultStatus { get; set; }
    
    public DateTime CreateDate { get; set; }
    
}