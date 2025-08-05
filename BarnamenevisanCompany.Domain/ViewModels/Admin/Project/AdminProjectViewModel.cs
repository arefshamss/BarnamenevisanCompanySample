namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Project;

public class AdminProjectViewModel
{
    public short Id { get; set; }
    public string Title { get; set; }
    public short Priority { get; set; }
    
    
    public string Url { get; set; }
    
    public string ImageName { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }


}