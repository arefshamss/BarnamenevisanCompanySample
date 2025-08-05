namespace BarnamenevisanCompany.Domain.ViewModels.Client.User;

public class ClientUserViewModel
{
    public int Id { get; set; } 
    public string? AvatarImageName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Mobile { get; set; }
    public DateTime CreatedDate { get; set; }

    #region Helper

    public string? FullName => string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName) ? null : $"{FirstName} {LastName}";

    #endregion
}