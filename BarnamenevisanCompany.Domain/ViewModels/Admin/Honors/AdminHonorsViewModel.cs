namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Honors;

public class AdminHonorsViewModel
{
    public short HonorId { get; set; }
    
    public string Title { get; set; }

    public string ImageName { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
    
    
    
}