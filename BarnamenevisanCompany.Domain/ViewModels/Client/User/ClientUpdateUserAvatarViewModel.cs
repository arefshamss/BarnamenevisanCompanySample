using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.User;

public class ClientUpdateUserAvatarViewModel
{
    public int UserId { get; set; }
    public IFormFile Avatar { get; set; }
    public string? AvatarImageName { get; set; }     
}