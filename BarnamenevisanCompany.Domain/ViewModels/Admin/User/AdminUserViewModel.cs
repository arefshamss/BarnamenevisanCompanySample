namespace BarnamenevisanCompany.Domain.ViewModels.Admin.User;

public class AdminUserViewModel
{
    public int Id { get; set; } 
    public string? AvatarImageName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Mobile { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }  
    public bool IsDeleted { get; set; }

    #region Helpers

    public string FullName => $"{FirstName} {LastName}";

    #endregion
}