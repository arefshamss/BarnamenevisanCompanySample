namespace BarnamenevisanCompany.Domain.ViewModels.Admin.TicketPriority;

public class AdminTicketPriorityViewModel
{
    public short Id { get; set; }   
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; }   
    public bool IsDeleted { get; set; } 
}