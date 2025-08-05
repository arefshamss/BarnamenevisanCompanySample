namespace BarnamenevisanCompany.Domain.ViewModels.Client.User;

public class ConfirmLoginViewModel
{
    public int UserId { get; set; }
    public string Mobile { get; set; }
    public string FullName { get; set; }
    public string? ActiveCode { get; set; }     
    public bool IsLoginByPassword { get; set; } 
}