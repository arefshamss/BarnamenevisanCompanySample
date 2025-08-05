using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Role;

public class AdminCreateRoleViewModel
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(100, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Name { get; set; }
}