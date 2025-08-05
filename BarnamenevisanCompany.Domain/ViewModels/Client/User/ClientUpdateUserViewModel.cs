using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.User;

public class ClientUpdateUserViewModel
{
    public int Id { get; set; }
    
    
    [Display(Name = "نام")]
    [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? FirstName { get; set; }
    
    [Display(Name = "نام خانوادگی")]
    [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? LastName { get; set; }     
    
    public string? Mobile { get; set; }
}