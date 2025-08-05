namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Consult;

public class AdminConsultViewModel
{
    public short Id { get; set; }
    
    public string UserFullName { get; set; }

    public DateTime CreateDate { get; set; }
    
    public bool IsDeleted { get; set; }
    public string PhoneNumber { get; set; }

    public string? AdminMessage { get; set; }

    public string Title { get; set; }
}